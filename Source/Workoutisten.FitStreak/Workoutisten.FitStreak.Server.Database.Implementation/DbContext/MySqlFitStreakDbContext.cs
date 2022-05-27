using Microsoft.EntityFrameworkCore;

namespace Workoutisten.FitStreak.Server.Database.Implementation.DbContext
{
    public class MySqlFitStreakDbContext : FitStreakDbContext
    {
        public MySqlFitStreakDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
