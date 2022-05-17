using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Workoutisten.FitStreak.Server.Database.Implementation.Exceptions;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;

public class TokenService : ITokenService
{
    public TokenService(IConfiguration configuration, IRepository repository)
    {
        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        _TokenSecret = configuration["JwtBearer:TokenSecret"] ?? throw new ArgumentException("JwtBearer:TokenSecret value is null", nameof(configuration));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    private string _TokenSecret;

    public IRepository Repository { get; }

    public Task<TokenResult> GenerateTokensAsync(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var jwt = CreateJwt(user);
        var refreshToken = CreateRefreshToken();

        var tokenResult = new TokenResult
        {
            Jwt = jwt,
            RefreshToken = refreshToken
        };

        return Task.FromResult(tokenResult);
    }

    public async Task<Result<User>> GetUserFromJwtAsync(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_TokenSecret)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return new Result<User>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Detail = "Invalid token!"
            };
        }

        var userIdString = principal.Claims.SingleOrDefault(c => c.Type == "UserId")?.Value;

        if (userIdString is null)
        {
            return new Result<User> 
            { 
                StatusCode = StatusCodes.Status404NotFound,
                Detail = "There was no userId in the claim!"
            };
        }
        if (!Guid.TryParse(userIdString, out var userId))
        {
            return new Result<User>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Detail = "The userId was not a Guid!"
            };
        }

        try
        {
            var user =  await Repository.GetAsync<User>(userId);
            if (user is null)
            {
                return new Result<User>
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Detail = "No user exists with the given id."
                };
            }
            else
            {
                return new Result<User>
                {
                    Value = user,
                    StatusCode = StatusCodes.Status200OK
                };
            }
        } catch (DatabaseRepositoryException)
        {
            return new Result<User>
            {
                StatusCode = StatusCodes.Status503ServiceUnavailable,
                Detail = "The Database - Service couldn't connect to the Database."
            };
        }
    }

    public Task<bool> IsRefreshTokenValidAsync(User user, string refreshToken)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new ArgumentException($"'{nameof(refreshToken)}' cannot be null or empty.", nameof(refreshToken));
        }

        return Task.FromResult(user.RefreshToken == refreshToken && user.RefreshTokenExpiryTime > DateTime.UtcNow);
    }

    private string CreateJwt(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_TokenSecret);

        var listClaims = new List<Claim>()
            {
                new("UserId", user.Id.ToString())
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(listClaims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string CreateRefreshToken()
    {
        var randomNumber = new byte[64];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
}
