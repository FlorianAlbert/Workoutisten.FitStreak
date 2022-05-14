using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

    private string TokenSecret;

    public AuthenticationService(IRepository repository, IPasswordHashingService passwordHashingService, IConfiguration configuration)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        PasswordHashingService = passwordHashingService ?? throw new ArgumentNullException(nameof(passwordHashingService));
        TokenSecret = configuration["JwtBearer:TokenSecret"];
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
            Token = await GenerateToken(user)
        };

        return new LoginResult { Status = LoginResultStatus.Unauthorized };
    }

    public Task<string> GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(TokenSecret);

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
        return Task.FromResult(tokenHandler.WriteToken(token));
    }
}
