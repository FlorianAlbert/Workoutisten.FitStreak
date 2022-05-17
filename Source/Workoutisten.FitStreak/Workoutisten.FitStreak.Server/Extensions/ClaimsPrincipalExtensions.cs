using System.Security.Claims;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static async Task<User?> GetDbUserAsync(this ClaimsPrincipal principal, IRepository repository)
        {
            var userId = await principal.GetUserIdAsync();
            if (userId is null) return null;
            else return await repository.GetAsync<User>(userId.Value);
        }

        public static async Task<bool> IsUserAllowedTo(this ClaimsPrincipal principal, IRepository repository, Func<User, IRepository, bool> isAllowed)
        {
            var user = await principal.GetDbUserAsync(repository);

            if (user is null) return false;

            return isAllowed(user, repository);
        }

        public static Task<Guid?> GetUserIdAsync(this ClaimsPrincipal principal)
        {
            if (principal is null) throw new ArgumentNullException(nameof(principal));

            var userIdString = principal.Claims.SingleOrDefault(c => c.Type == "UserId")?.Value;

            if (userIdString is null ||
                !Guid.TryParse(userIdString, out var userId)) 
                return Task.FromResult((Guid?) null);
            else return Task.FromResult((Guid?) userId);
        }
    }
}
