namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

public interface IEmailService
{
    Task BroadcastEmail(IEnumerable<string> emailAddresses, string subject, string message);

    Task SendEmail(string emailAddress, string subject, string message);
}
