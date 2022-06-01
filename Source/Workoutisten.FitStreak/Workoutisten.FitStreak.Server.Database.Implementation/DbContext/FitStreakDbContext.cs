using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Workoutisten.FitStreak.Server.Database.Implementation.Configurations;
using Workoutisten.FitStreak.Server.Database.Implementation.Extensions;
using Workoutisten.FitStreak.Server.Database.Implementation.Trigger;
using Workoutisten.FitStreak.Server.Model;
using BaseDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Workoutisten.FitStreak.Server.Database.Implementation.DbContext
{
    public class FitStreakDbContext : BaseDbContext
    {
        public FitStreakDbContext(DbContextOptions options) : base(options)
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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var selectedEntities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity &&
                        (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in selectedEntities)
            {
                if (entity.State == EntityState.Added)
                {
                    if (entity.Entity is BaseEntity baseEntity)
                    {
                        baseEntity.CreatedAt = DateTime.UtcNow;
                        baseEntity.LastUpdated = DateTime.UtcNow;
                    }
                }

                if (entity.State == EntityState.Modified)
                {
                    if (entity.Entity is BaseEntity baseEntity)
                    {
                        baseEntity.LastUpdated = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
