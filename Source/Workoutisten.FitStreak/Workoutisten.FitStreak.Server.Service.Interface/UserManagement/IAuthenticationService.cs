using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IAuthenticationService
{
    Task<Result<LoginResult>> LoginAsync(string email, string password);

    Task<Result<TokenResult>> RefreshTokens(string expiredJwt, string refreshToken);
}
