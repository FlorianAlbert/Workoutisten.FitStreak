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
            switch (context.ChangeType)
            {
                case ChangeType.Modified:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} got successfully updated.",
                        context.Entity.GetTableName(DbContext),
                        context.Entity.Id);
                    break;
                case ChangeType.Deleted:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} got successfully deleted.",
                        context.Entity.GetTableName(DbContext),
                        context.Entity.Id);
                    break;
                case ChangeType.Added:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} got successfully created.",
                        context.Entity.GetTableName(DbContext),
                        context.Entity.Id);
                    break;
            }

            return Task.CompletedTask;
        }

        public Task AfterSaveFailed(ITriggerContext<BaseEntity> context, Exception exception, CancellationToken cancellationToken)
        {
            switch (context.ChangeType)
            {
                case ChangeType.Modified:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} failed to get updated.",
                        context.Entity.GetTableName(DbContext),
                        context.Entity.Id);
                    break;
                case ChangeType.Deleted:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} failed to get deleted.",
                        context.Entity.GetTableName(DbContext),
                        context.Entity.Id);
                    break;
                case ChangeType.Added:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} failed to get created.",
                        context.Entity.GetTableName(DbContext),
                        context.Entity.Id);
                    break;
            }

            return Task.CompletedTask;
        }

        public Task BeforeSave(ITriggerContext<BaseEntity> context, CancellationToken cancellationToken)
        {
            switch (context.ChangeType)
            {
                case ChangeType.Modified:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} is getting updated.", 
                        context.UnmodifiedEntity.GetTableName(DbContext), 
                        context.UnmodifiedEntity.Id);
                    break;
                case ChangeType.Deleted:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} is getting deleted.",
                        context.UnmodifiedEntity.GetTableName(DbContext),
                        context.UnmodifiedEntity.Id);
                    break;
                case ChangeType.Added:
                    Logger.LogInformation("Entity in table {tableName} with Id={id} is getting created.",
                        context.UnmodifiedEntity.GetTableName(DbContext),
                        context.UnmodifiedEntity.Id);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
