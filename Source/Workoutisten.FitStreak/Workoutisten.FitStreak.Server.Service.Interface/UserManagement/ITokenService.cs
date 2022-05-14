using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(User user);
}
