using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Workoutisten.FitStreak.Server.Database.Implementation.Configurations;

namespace Workoutisten.FitStreak.Server.Database
{
    public class FitStreakDbContext : DbContext
    {
        public FitStreakDbContext(DbContextOptions<FitStreakDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var allTypes = Assembly.GetAssembly(typeof(BaseEntityConfigurations<>)).GetTypes();
            var allEntityConfigurationsTypes = from x in allTypes
                                        let y = x.BaseType
                                        where !x.IsAbstract && 
                                              !x.IsInterface &&
                                              y != null && 
                                              y.IsGenericType &&
                                              !y.IsInterface &&
                                              y.GetGenericTypeDefinition() == typeof(BaseEntityConfigurations<>)
                                        select x;

            foreach (var type in allEntityConfigurationsTypes)
            {
                var constructor = type.GetConstructor(new Type[] { });

                if (constructor is not null)
                {
                    modelBuilder.ApplyConfiguration(constructor.Invoke(new object[] { }) as dynamic);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
