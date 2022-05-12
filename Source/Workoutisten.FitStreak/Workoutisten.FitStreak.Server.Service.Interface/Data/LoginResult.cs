using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Service.Interface.Data;
public class LoginResult
{
    public string Token { get; set; }

    public User User { get; set; }

    public LoginResultStatus Status { get; set; }
}
