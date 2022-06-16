using EntityFrameworkCore.Triggered;
using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Trigger
{
    public class OnBeforeModifiedUserEntity : IBeforeSaveTrigger<User>
    {
        public Task BeforeSave(ITriggerContext<User> context, CancellationToken cancellationToken)
        {
            switch (context.ChangeType)
            {
                case ChangeType.Added:
                    BeforeUserAdded(context);
                    break;
                case ChangeType.Modified:
                    BeforeUserChanged(context);
                    break;
            }

            return Task.CompletedTask;
        }

        private void BeforeUserChanged(ITriggerContext<User> context)
        {
            if (context.Entity.Streak < 0)
                context.Entity.Streak = 0; 
            
            if (context.Entity.MaxStreak < 0)
                context.Entity.MaxStreak = 0;
        }

        private void BeforeUserAdded(ITriggerContext<User> context)
        {
            context.Entity.Streak = 0;
            context.Entity.MaxStreak = 0;
        }
    }
}
