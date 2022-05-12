using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IRegistrationService
{
    Task<bool> RequestRegistrationAsync(string email, string password);

    Task<bool> ConfirmRegistrationAsync(Guid confirmationId);
}
