using Microsoft.AspNetCore.Http;
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

    public async Task<Result<LoginResult>> LoginAsync(string email, string password)
    {
        IEnumerable<User> users;

        try
        {
            users = await Repository.GetAllAsync<User>();
        } 
        catch (DatabaseRepositoryException)
        {
            return new Result<LoginResult> 
            { 
                StatusCode = StatusCodes.Status503ServiceUnavailable ,
                Detail = "The Database-Service couldn't connect to the Database."
            };
        }

        var user = users.FirstOrDefault(user => user.NormalizedEmail == email.NormalizeEmail());
        if (user is null)
        {
            return new Result<LoginResult>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Detail = $"There exists no registered user with the email {email}."
            };
        }

        if (!user.IsVerified)
        {
            return new Result<LoginResult>
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Detail = $"The user with the email {email} first has to be verified!"
            };
        }

        var successful = await PasswordHashingService.VerifyPasswordAsync(password, user.PasswordHash);
        if (!successful) 
        {
            return new Result<LoginResult>
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Detail = $"The password for the user with the email {email} was wrong!"
            };
        }

        

        var tokens = await TokenService.GenerateTokensAsync(user);

        try
        {
            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(3);
            user.PasswordForgottenKey = null; //resets key in case the reset was requested but remembered it before executing reset
            await Repository.CreateOrUpdateAsync(user);

            return new Result<LoginResult>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = new LoginResult
                {
                    User = user,
                    Tokens = tokens
                }
            };
        }
        catch (DatabaseRepositoryException)
        {
            return new Result<LoginResult>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database-Service couldn't connect to the Database."
            };
        }
    }

    public async Task<Result<TokenResult>> RefreshTokens(string expiredJwt, string refreshToken)
    {
        var userResult = await TokenService.GetUserFromJwtAsync(expiredJwt);
        if (userResult.Unsccessful) {
            return new Result<TokenResult>
            {
                StatusCode = userResult.StatusCode,
                Detail = userResult.Detail
            };
        }

        var user = userResult.Value;
        var isRefreshTokenValid = await TokenService.IsRefreshTokenValidAsync(user, refreshToken);
        if (!isRefreshTokenValid)
        {
            return new Result<TokenResult>
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Detail = "The refreshToken is expired!"
            };
        }

        var tokens = await TokenService.GenerateTokensAsync(user);
        if(tokens?.RefreshToken is null || tokens.Jwt is null)
        {
            return new Result<TokenResult>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The server was unable to generate new tokens with the correct refreshToken."
            };
        }

        try
        {
            user.RefreshToken = tokens.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(3);
            await Repository.CreateOrUpdateAsync(user);
            return new Result<TokenResult>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = tokens
            };

        } catch (DatabaseRepositoryException)
        {
            return new Result<TokenResult>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }
}
