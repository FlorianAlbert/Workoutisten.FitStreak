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

    public RegistrationService(IRepository repository, IPasswordHashingService passwordHashingService)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        PasswordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
    }

    public async Task<Result<bool>> CanRegisterAsync(string email)
    {
        try
        {
            var users = await Repository.GetAllAsync<User>();
            var userWithEmailExists = !users.Any(user => user.NormalizedEmail.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            return new Result<bool> { Data = userWithEmailExists, Status = ResultStatus.Successful };
        } catch (DatabaseRepositoryException)
        {
            return new Result<bool> { Status = ResultStatus.ServerError};
        }
    }

    public async Task<Result<bool>> ConfirmRegistrationAsync(Guid userId)
    {
        try
        {
            var user = await Repository.GetAsync<User>(userId);
            if (user is null) return new Result<bool> { Status = ResultStatus.BadRequest };

            user.IsVerified = true;
            var updatedUser = await Repository.CreateOrUpdateAsync(user);
            return new Result<bool> { Data = updatedUser.IsVerified, Status = ResultStatus.Successful };
        } catch (DatabaseRepositoryException)
        {
            return new Result<bool> { Status = ResultStatus.ServerError };
        }
    }

    public async Task<Result<bool>> RegisterAsync(string email, string password, string firstName, string lastName)
    {
        var user = new User
        {
            Id = Guid.Empty,
            FirstName = firstName,
            LastName = lastName,
            NormalizedEmail = email.NormalizeEmail(),
            PasswordHash = await PasswordHashingService.HashPasswordForStorageAsync(password)
        };

        try
        {
            var storedUser = await Repository.CreateOrUpdateAsync(user);
            return new Result<bool> { Data = storedUser.Id != Guid.Empty, Status = ResultStatus.Successful };
        } catch (DatabaseRepositoryException)
        {
            return new Result<bool> { Status = ResultStatus.ServerError };
        }
    }
}
