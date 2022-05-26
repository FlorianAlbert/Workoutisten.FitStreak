using EntityFrameworkCore.Triggered;
using Microsoft.Extensions.Logging;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Trigger;

public class AfterSuccessfullySaveEmail : IAfterSaveTrigger<Email>
{
    public ILogger<AfterSuccessfullySaveEmail> Logger { get; }
    private IEmailService EmailService { get; }

    public AfterSuccessfullySaveEmail(ILogger<AfterSuccessfullySaveEmail> logger,IEmailService emailService)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        EmailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public async Task AfterSave(ITriggerContext<Email> context, CancellationToken cancellationToken)
    {
        if(context.ChangeType is ChangeType.Added)
        {
            Logger.LogInformation($"Email with Id={context.Entity} is getting sent.");

            var subject = context.Entity.Subject;
            var message = context.Entity.Message;
            var receivers = context.Entity.Receivers;
            await EmailService.BroadcastEmail(receivers, subject, message);

            Logger.LogInformation($"Email with Id={context.Entity} is got sent.");
        }
    }
}
