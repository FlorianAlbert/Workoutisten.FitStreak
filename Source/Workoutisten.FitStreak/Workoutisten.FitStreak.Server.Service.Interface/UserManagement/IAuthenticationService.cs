using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IAuthenticationService
{
    Task<LoginResult> LoginAsync(string email, string password);
}
