using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

public interface ITokenService
{
    Task<TokenResult> GenerateTokensAsync(User user);

    Task<User?> GetUserFromJwtAsync(string token);

    Task<bool> IsRefreshTokenValidAsync(User user, string refreshToken);
}
