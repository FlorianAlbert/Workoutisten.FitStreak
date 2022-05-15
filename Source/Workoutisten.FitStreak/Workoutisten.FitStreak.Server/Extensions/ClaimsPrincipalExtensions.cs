using System.Security.Claims;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static async Task<User?> GetDbUserAsync(this ClaimsPrincipal principal, IRepository repository)
        {
            if (principal is null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            if (repository is null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            var userIdString = principal.Claims.SingleOrDefault(c => c.Type == "UserId")?.Value;

            if (userIdString is null) return null;

            if (!Guid.TryParse(userIdString, out var userId)) return null;

            return await repository.GetAsync<User>(userId);
        }

        public static async Task<bool> IsUserAllowedTo(this ClaimsPrincipal principal, IRepository repository, Func<User, IRepository, bool> isAllowed)
        {
            var user = await principal.GetDbUserAsync(repository);

            if (user is null) return false;

            return isAllowed(user, repository);
        }
    }
}
