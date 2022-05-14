using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Implementation.Extension;
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

    public async Task<bool> CanRegisterAsync(string email)
    {
        var users = await Repository.GetAllAsync<User>();
        return !users.Any(user => user.NormalizedEmail.Equals(email, StringComparison.InvariantCultureIgnoreCase));
    }

    public async Task<bool> ConfirmRegistrationAsync(Guid userId)
    {
        var user = await Repository.GetAsync<User>(userId);
        if (user is null) return false;

        user.IsVerified = true;
        var updatedUser = await Repository.CreateOrUpdateAsync(user);
        return updatedUser.IsVerified;
    }

    public async Task<bool> RegisterAsync(string email, string password)
    {
        var user = new User
        {
            Id = Guid.Empty,
            FirstName = string.Empty,
            LastName = string.Empty,
            NormalizedEmail = email.NormalizeEmail(),
            PasswordHash = await PasswordHashingService.HashPasswordForStorageAsync(password)
        };

        var storedUser = await Repository.CreateOrUpdateAsync(user);
        return storedUser.Id != Guid.Empty;
    }
}
