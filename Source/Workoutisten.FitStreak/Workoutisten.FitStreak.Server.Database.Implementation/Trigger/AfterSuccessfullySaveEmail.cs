
using EntityFrameworkCore.Triggered;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Trigger;

public class AfterSuccessfullySaveEmail : IAfterSaveTrigger<Email>
{
    private IEmailService EmailService { get; }

    public AfterSuccessfullySaveEmail(IEmailService emailService)
    {
        EmailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public async Task AfterSave(ITriggerContext<Email> context, CancellationToken cancellationToken)
    {
        if(context.ChangeType is ChangeType.Added)
        {
            var subject = context.Entity.Subject;
            var message = context.Entity.Message;
            var emailAddresses = context.Entity.Receivers.Select(user => user.NormalizedEmail);
            await EmailService.BroadcastEmail(emailAddresses, subject, message);
        }
    }
}
