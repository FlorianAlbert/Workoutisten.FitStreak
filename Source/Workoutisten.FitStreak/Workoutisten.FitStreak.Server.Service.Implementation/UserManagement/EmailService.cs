using FluentEmail.Core;
using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;

public class EmailService : IEmailService
{
    private IFluentEmailFactory Factory { get; }

    public EmailService(IFluentEmailFactory factory)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
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
        var newMail = Factory.Create();

        newMail.To(receiver.NormalizedEmail, $"{receiver.FirstName} {receiver.LastName}")
               .Subject(subject)
               .Body(message);

        var response = await newMail.SendAsync();
    }
}
