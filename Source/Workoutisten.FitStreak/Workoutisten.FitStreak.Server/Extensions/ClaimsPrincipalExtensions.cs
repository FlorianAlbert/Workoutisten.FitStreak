﻿using System.Security.Claims;
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

            var userIdString = principal.Claims.SingleOrDefault(c => c.Type == "UserId")?.Value;

            if (userIdString is null) return null;

            if (!Guid.TryParse(userIdString, out var userId)) return null;

            return await repository.GetAsync<User>(userId);
        }
    }
}
