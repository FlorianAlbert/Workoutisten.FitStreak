using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

public interface IEmailService
{
    Task BroadcastEmail(IEnumerable<User> receivers, string subject, string message);

    Task SendEmail(User receiver, string subject, string message);
}
