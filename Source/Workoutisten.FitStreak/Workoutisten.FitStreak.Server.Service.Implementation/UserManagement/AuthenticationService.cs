using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Implementation.Extension;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class AuthenticationService : IAuthenticationService
{
    private IRepository Repository { get; }

    private IPasswordHashingService PasswordHashingService { get; }

   private ITokenService TokenService { get; }

    public AuthenticationService(IRepository repository, IPasswordHashingService passwordHashingService, ITokenService tokenService)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        PasswordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
        TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        var users = await Repository.GetAllAsync<User>();

        var user = users.FirstOrDefault(user => user.NormalizedEmail == email.NormalizeEmail());
        if (user is null || !user.IsVerified) return new LoginResult { Status = LoginResultStatus.BadRequest };

        var successful = await PasswordHashingService.VerifyPasswordAsync(password, user.PasswordHash);
        if (successful) return new LoginResult
        {
            Status = LoginResultStatus.Successful,
            User = user,
            //Token = await TokenService.GenerateTokensAsync(user)
        };

        return new LoginResult { Status = LoginResultStatus.Unauthorized };
    }
}
