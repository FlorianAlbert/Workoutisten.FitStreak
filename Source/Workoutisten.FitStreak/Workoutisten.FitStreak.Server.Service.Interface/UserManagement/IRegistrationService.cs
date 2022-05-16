using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IRegistrationService
{
    Task<Result<bool>> CanRegisterAsync(string email);

    Task<Result<bool>> ConfirmRegistrationAsync(Guid userId);

    Task<Result> RegisterAsync(string email, string password, string firstName, string lastName);
}
