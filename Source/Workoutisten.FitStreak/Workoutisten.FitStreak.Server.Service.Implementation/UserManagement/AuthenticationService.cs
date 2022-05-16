using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
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
        IEnumerable<User> users;

        try
        {
            users = await Repository.GetAllAsync<User>();
        } catch (DatabaseRepositoryException)
        {
            return new LoginResult { Status = ResultStatus.ServerError };
        }

        var user = users.FirstOrDefault(user => user.NormalizedEmail == email.NormalizeEmail());
        if (user is null || !user.IsVerified) return new LoginResult { Status = ResultStatus.BadRequest };

        var successful = await PasswordHashingService.VerifyPasswordAsync(password, user.PasswordHash);
        if (successful) 
        {
            var tokens = await TokenService.GenerateTokensAsync(user);

            return new LoginResult
            {
                Status = ResultStatus.Successful,
                User = user,
                RefreshToken = tokens.RefreshToken,
                Jwt = tokens.Jwt
            }; 
        }

        return new LoginResult { Status = ResultStatus.Unauthorized };
    }
}
