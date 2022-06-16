using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Workoutisten.FitStreak.Server.Database.Implementation.DbContext
{
    public class MsSqlFitStreakDbContext : FitStreakDbContext
    {
        public MsSqlFitStreakDbContext(DbContextOptions<MsSqlFitStreakDbContext> options) : base(options)
        {
        }
    }
}
