using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Workoutisten.FitStreak.Server.Database.Implementation.DbContext
{
    public class MySqlFitStreakDbContext : FitStreakDbContext
    {
        public MySqlFitStreakDbContext(DbContextOptions<MySqlFitStreakDbContext> options) : base(options)
        {
        }
    }
}
