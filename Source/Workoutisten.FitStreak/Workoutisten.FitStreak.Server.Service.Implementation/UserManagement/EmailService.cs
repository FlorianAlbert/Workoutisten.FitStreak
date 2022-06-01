using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;

public class EmailService : IEmailService
{
    private IFluentEmailFactory Factory { get; }

    private ILogger<EmailService> Logger { get; }

    public EmailService(IFluentEmailFactory factory, ILogger<EmailService> logger)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task BroadcastEmail(IEnumerable<User> receivers, string subject, string message)
    {
        foreach(var receiver in receivers)
        {
            await SendEmail(receiver, subject, message);
        }
    }

    public async Task SendEmail(User receiver, string subject, string message)
    {
        try
        {
            var newMail = Factory.Create();

            newMail.To(receiver.NormalizedEmail, $"{receiver.FirstName} {receiver.LastName}")
                   .Subject(subject)
                   .Body(message);

            var response = await newMail.SendAsync();
        } 
        catch
        {
            Logger.LogError($"Email with subject \"{subject}\" couldn't be send to \"{receiver.NormalizedEmail}\"!");
        }
    }
}
