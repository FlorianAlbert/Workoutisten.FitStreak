using Microsoft.AspNetCore.Http;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Service.Implementation.Extension;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
using User = Workoutisten.FitStreak.Server.Model.Account.User;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class PasswordService : IPasswordService
{
    private IRepository Repository { get; }

    private IAlphaNumericStringGenerator AlphaNumericStringGenerator { get; }

    private IPasswordHashingService PasswordHashingService { get; }

    public PasswordService(IRepository repository, IAlphaNumericStringGenerator alphaNumericStringGenerator, IPasswordHashingService passwordHashingService)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        AlphaNumericStringGenerator = alphaNumericStringGenerator ?? throw new ArgumentNullException(nameof(alphaNumericStringGenerator));
        PasswordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
    }

    public async Task<Result> ChangePasswordAsync(Guid userId, string email, string oldPassword, string newPassword)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no registered user with your id!"
                };
            }

            if (user.NormalizedEmail != email.NormalizeEmail() ||
                !await PasswordHashingService.VerifyPasswordAsync(oldPassword, user.PasswordHash))
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"Your combination of id, email and oldPassword don't authorize you to change the password for the email {email}!"
                };
            }

            user.PasswordHash = await PasswordHashingService.HashPasswordForStorageAsync(newPassword);
            await Repository.CreateOrUpdateAsync(user);

            return new Result
            {
                StatusCode = StatusCodes.Status204NoContent
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

    public async Task<Result> RequestPasswordResetAsync(string email)
    {
        try
        {
            var users = await Repository.GetAllAsync<User>();
            var user = users.SingleOrDefault(user => user.NormalizedEmail == email.NormalizeEmail());
            if (user is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no registered user with the email {email}."
                };
            }

            user.PasswordForgottenKey = await AlphaNumericStringGenerator.GenerateAlphaNumericString();
            await Repository.CreateOrUpdateAsync(user);
            return new Result 
            { 
                StatusCode = StatusCodes.Status204NoContent 
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

    public async Task<Result> ResetPasswordAsync(string passwordForgottenKey, string email, string newPassword)
    {
        try
        {
            var users = await Repository.GetAllAsync<User>();
            var user = users.SingleOrDefault(user => user.NormalizedEmail == email.NormalizeEmail());
            if (user is null)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There exists no registered user with the email {email}."
                };
            }

            if (user.PasswordForgottenKey != passwordForgottenKey)
            {
                return new Result
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Detail = $"The passwordForgottenKey {passwordForgottenKey} isn't valid for the user with the email {email}."
                };
            }

            user.PasswordForgottenKey = null;
            user.PasswordHash = await PasswordHashingService.HashPasswordForStorageAsync(newPassword);
            await Repository.CreateOrUpdateAsync(user);

            return new Result
            {
                StatusCode = StatusCodes.Status204NoContent
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
}
