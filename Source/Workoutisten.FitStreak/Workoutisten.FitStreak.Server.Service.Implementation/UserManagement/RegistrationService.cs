using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class RegistrationService : IRegistrationService
{
    public async Task<bool> ConfirmRegistrationAsync(Guid confirmationId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RequestRegistrationAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}
