using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;

public class EmailService : IEmailService
{
    private string SmtpHost { get; }
    private int SmtpPort { get; }
    private string SmtpUser { get; }
    private string SmtpPass { get; }

    private readonly ILogger<EmailService> Logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        if (string.IsNullOrEmpty(configuration[$"Smtp:{nameof(SmtpHost)}"]) ||
            string.IsNullOrEmpty(configuration[$"Smtp:{nameof(SmtpPort)}"]) ||
            string.IsNullOrEmpty(configuration[$"Smtp:{nameof(SmtpUser)}"]) ||
            string.IsNullOrEmpty(configuration[$"Smtp:{nameof(SmtpPass)}"]))
        {
            throw new ArgumentException("Smtp:<Values> must be not null!",
                nameof(configuration));
        }

        SmtpHost = configuration[$"Smtp:{nameof(SmtpHost)}"];

        if (int.TryParse(configuration[$"Smtp:{nameof(SmtpPort)}"], out var port)) SmtpPort = port;
        else throw new ArgumentException($"Smtp:{ nameof(SmtpPort) } value does not contain an integer.", nameof(configuration));

        SmtpUser = configuration[$"Smtp:{nameof(SmtpUser)}"];
        SmtpPass = configuration[$"Smtp:{nameof(SmtpPass)}"];
    }

    public async Task BroadcastEmail(IEnumerable<string> emailAddresses, string subject, string message)
    {
        foreach(var emailAddress in emailAddresses)
        {
            await SendEmail(emailAddress, subject, message);
        }
    }

    public async Task SendEmail(string emailAddress, string subject, string message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(SmtpUser));
        email.To.Add(MailboxAddress.Parse(emailAddress));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Plain) { Text = message };

        // send email
        try
        {
            using var smtp = new SmtpClient();
            smtp.Connect(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(SmtpUser, SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        } 
        catch (Exception e)
        {
            Logger.LogInformation(e, $"Could not send email with subject {subject} to {emailAddress}.");
        }
    }
}
