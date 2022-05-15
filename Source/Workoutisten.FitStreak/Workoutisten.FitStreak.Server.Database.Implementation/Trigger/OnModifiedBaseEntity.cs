using EntityFrameworkCore.Triggered;
using Microsoft.Extensions.Logging;
using Workoutisten.FitStreak.Server.Database.Implementation.Extensions;
using Workoutisten.FitStreak.Server.Model;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Trigger
{
    public class OnModifiedBaseEntity : IAfterSaveTrigger<BaseEntity>, IBeforeSaveTrigger<BaseEntity>, IAfterSaveFailedTrigger<BaseEntity>
    {
        private ILogger<OnModifiedBaseEntity> Logger { get; }
        private FitStreakDbContext DbContext { get; }

        public OnModifiedBaseEntity(ILogger<OnModifiedBaseEntity> logger, FitStreakDbContext dbContext)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task AfterSave(ITriggerContext<BaseEntity> context, CancellationToken cancellationToken)
        {
            var entity = context.Entity ?? context.UnmodifiedEntity;

            switch (context.ChangeType)
            {
                case ChangeType.Modified:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} got successfully updated.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Deleted:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} got successfully deleted.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Added:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} got successfully created.",
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
                    Logger.LogError("Entity in table {tableName} with Id={id} failed to get updated.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Deleted:
                    Logger.LogError("Entity in table {tableName} with Id={id} failed to get deleted.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Added:
                    Logger.LogError("Entity in table {tableName} with Id={id} failed to get created.",
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
                    Logger.LogInformation("Entity in table {tableName} with Id={id} is getting updated.", 
                        entity.GetTableName(DbContext), 
                        entity.Id);
                    break;
                case ChangeType.Deleted:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} is getting deleted.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
                case ChangeType.Added:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} is getting created.",
                        entity.GetTableName(DbContext),
                        entity.Id);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
