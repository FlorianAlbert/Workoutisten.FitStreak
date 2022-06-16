using EntityFrameworkCore.Triggered;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Workoutisten.FitStreak.Server.Database.Implementation.DbContext;
using Workoutisten.FitStreak.Server.Database.Implementation.Extensions;
using Workoutisten.FitStreak.Server.Model;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Trigger
{
    public class OnModifiedBaseEntity : IAfterSaveTrigger<BaseEntity>, IBeforeSaveTrigger<BaseEntity>, IAfterSaveFailedTrigger<BaseEntity>
    {
        private ILogger<OnModifiedBaseEntity> _Logger;
        private FitStreakDbContext DbContext { get; }

        public OnModifiedBaseEntity(IServiceProvider serviceProvider, FitStreakDbContext dbContext)
        {
            if (serviceProvider is null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            _Logger = serviceProvider.GetRequiredService<ILogger<OnModifiedBaseEntity>>();
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task AfterSave(ITriggerContext<BaseEntity> context, CancellationToken cancellationToken)
        {
            var entity = context.Entity ?? context.UnmodifiedEntity;

            switch (context.ChangeType)
            {
                case ChangeType.Modified:
                    _Logger.LogInformation("Entity in table {tableName} with Id={id} got successfully updated.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Deleted:
                    _Logger.LogInformation("Entity in table {tableName} with Id={id} got successfully deleted.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Added:
                    _Logger.LogInformation("Entity in table {tableName} with Id={id} got successfully created.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
            }

            return Task.CompletedTask;
        }

        public Task AfterSaveFailed(ITriggerContext<BaseEntity> context, Exception exception, CancellationToken cancellationToken)
        {
            var entity = context.Entity ?? context.UnmodifiedEntity;

            switch (context.ChangeType)
            {
                case ChangeType.Modified:
                    _Logger.LogError("Entity in table {tableName} with Id={id} failed to get updated.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Deleted:
                    _Logger.LogError("Entity in table {tableName} with Id={id} failed to get deleted.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Added:
                    _Logger.LogError("Entity in table {tableName} with Id={id} failed to get created.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
            }

            return Task.CompletedTask;
        }

        public Task BeforeSave(ITriggerContext<BaseEntity> context, CancellationToken cancellationToken)
        {
            var entity = context.UnmodifiedEntity ?? context.Entity;

            switch (context.ChangeType)
            {
                case ChangeType.Modified:
                    _Logger.LogInformation("Entity in table {tableName} with Id={id} is getting updated.", 
                        entity.GetTableName(DbContext), 
                        entity.Id);
                    break;
                case ChangeType.Deleted:
                    _Logger.LogInformation("Entity in table {tableName} with Id={id} is getting deleted.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Added:
                    _Logger.LogInformation("Entity in table {tableName} with Id={id} is getting created.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
