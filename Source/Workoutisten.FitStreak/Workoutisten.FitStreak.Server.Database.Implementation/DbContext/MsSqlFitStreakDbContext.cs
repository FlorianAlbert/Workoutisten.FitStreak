using Microsoft.EntityFrameworkCore;

namespace Workoutisten.FitStreak.Server.Database.Implementation.DbContext
{
    public class MsSqlFitStreakDbContext : FitStreakDbContext
    {
        public MsSqlFitStreakDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
