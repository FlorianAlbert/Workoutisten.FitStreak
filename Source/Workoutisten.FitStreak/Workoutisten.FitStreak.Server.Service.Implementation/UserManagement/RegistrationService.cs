using Microsoft.AspNetCore.Http;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Implementation.Extension;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class RegistrationService : IRegistrationService
{
    private IRepository Repository { get; }

    private IPasswordHashingService PasswordHashingService { get; }

    private IAlphaNumericStringGenerator AlphaNumericStringGenerator { get; }

    public RegistrationService(IRepository repository, IPasswordHashingService passwordHashingService, IAlphaNumericStringGenerator alphaNumericStringGenerator)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        PasswordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
        AlphaNumericStringGenerator = alphaNumericStringGenerator ?? throw new ArgumentNullException(nameof(alphaNumericStringGenerator));
    }

    public async Task<Result<bool>> CanRegisterAsync(string email)
    {
        try
        {
            var users = await Repository.GetAllAsync<User>();
            var userWithEmailExists = users.Any(user => user.NormalizedEmail.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            if (userWithEmailExists)
            {
                return new Result<bool>
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Detail = $"There exists already a user with the email {email}!"
                };
            }
            else
            {
                return new Result<bool>
                {
                    Value = userWithEmailExists,
                    StatusCode = StatusCodes.Status200OK
                };
            }
            
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<bool>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<bool>> ConfirmRegistrationAsync(string registrationConfirmationKey)
    {
        try
        {
            var users = await Repository.GetAllAsync<User>();
            var user = users.FirstOrDefault(user => user.RegistrationConfirmationKey == registrationConfirmationKey);
            if (user is null) { 
                return new Result<bool> 
                { 
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = $"There is no registered user with this registrationConfirmationKey to confirm."
                }; 
            }

            user.IsVerified = true;
            user.RegistrationConfirmationKey = null;
            var updatedUser = await Repository.CreateOrUpdateAsync(user);
            return new Result<bool> 
            { 
                Value = updatedUser.IsVerified, 
                StatusCode = StatusCodes.Status200OK 
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<bool> 
            { 
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result> RegisterAsync(string email, string password, string firstName, string lastName)
    {
        var user = new User
        {
            Id = Guid.Empty,
            FirstName = firstName,
            LastName = lastName,
            NormalizedEmail = email.NormalizeEmail(),
            PasswordHash = await PasswordHashingService.HashPasswordForStorageAsync(password),
            RegistrationConfirmationKey = await AlphaNumericStringGenerator.GenerateAlphaNumericString()
        };

        try
        {
            var storedUser = await Repository.CreateOrUpdateAsync(user);
            return new Result 
            { 
                StatusCode = StatusCodes.Status200OK 
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
