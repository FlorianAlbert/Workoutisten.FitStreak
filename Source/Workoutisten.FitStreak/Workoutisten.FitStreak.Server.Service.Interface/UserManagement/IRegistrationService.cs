using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IRegistrationService
{
    Task<Result<bool>> CanRegisterAsync(string email);

    Task<Result> ConfirmRegistrationAsync(string email, string registrationConfirmationKey);

    Task<Result> RegisterAsync(string email, string password, string firstName, string lastName);
}
