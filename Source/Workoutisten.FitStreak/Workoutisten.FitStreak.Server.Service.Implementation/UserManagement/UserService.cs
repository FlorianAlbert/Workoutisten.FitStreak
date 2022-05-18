using Microsoft.AspNetCore.Http;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class UserService : IUserService
{
    private IRepository Repository { get; }

    public UserService(IRepository repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Result> DeleteUser(Guid ownUserId, Guid userToDeleteId)
    {
        if (ownUserId != userToDeleteId)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Detail = "You are not allowed to delete the account of someone else!"
            };
        }

        try
        {
            //ToDo delete related data!
            await Repository.DeleteAsync<User>(userToDeleteId);
            return new Result
            {
                StatusCode = StatusCodes.Status204NoContent
            };
        }
        catch (EntryNotFoundException)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"There exists no registered user with the id { userToDeleteId }."
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<User>> UpdateUserAsync(Guid userId, string? email = null, string? firstName = null, string? lastName = null)
    {
        try
        {
            var userToUpdate = await Repository.GetAsync<User>(userId);
            if (userToUpdate is null)
            {
                return new Result<User>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no registered user with the id {userId}."
                };
            }

            userToUpdate.NormalizedEmail = email?.Normalize() ?? userToUpdate.NormalizedEmail;
            userToUpdate.FirstName = firstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = lastName ?? userToUpdate.LastName;
            var updatedUser = await Repository.CreateOrUpdateAsync(userToUpdate);
            return new Result<User>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = userToUpdate
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<User>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }
}
