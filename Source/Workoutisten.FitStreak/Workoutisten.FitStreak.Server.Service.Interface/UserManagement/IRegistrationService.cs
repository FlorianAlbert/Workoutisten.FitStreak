namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IRegistrationService
{
    Task<bool> CanRegisterAsync(string email);

    Task<bool> ConfirmRegistrationAsync(Guid userId);

    Task<bool> RegisterAsync(string email, string password, string firstName, string lastName);
}
