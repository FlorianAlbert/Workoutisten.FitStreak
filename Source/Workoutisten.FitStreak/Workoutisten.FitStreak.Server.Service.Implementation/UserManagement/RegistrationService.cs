using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class RegistrationService : IRegistrationService
{
    private IRepository Repository { get; }

    public RegistrationService(IRepository repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> CanRegisterAsync(string email)
    {
        var users = await Repository.GetAllAsync<User>();
        return users.Any(user => user.NormalizedEmail.Equals(email, StringComparison.InvariantCultureIgnoreCase));
    }

    public async Task<bool> ConfirmRegistrationAsync(Guid confirmationId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RegisterAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}
