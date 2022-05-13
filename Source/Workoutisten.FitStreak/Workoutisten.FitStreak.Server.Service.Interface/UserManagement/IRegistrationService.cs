namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IRegistrationService
{
    Task<bool> CanRegisterAsync(string email);

    Task<bool> ConfirmRegistrationAsync(Guid confirmationId);

    Task<bool> RegisterAsync(string email, string password);
}
