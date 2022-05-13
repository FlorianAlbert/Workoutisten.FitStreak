using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class RegistrationService : IRegistrationService
{
    public async Task<bool> CanRegisterAsync(string email)
    {
        throw new NotImplementedException();
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
