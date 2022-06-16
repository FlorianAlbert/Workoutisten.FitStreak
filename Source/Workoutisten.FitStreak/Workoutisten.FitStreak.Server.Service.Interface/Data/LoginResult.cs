using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Service.Interface.Data;
public class LoginResult
{
    public User User { get; set; }

    public TokenResult Tokens { get; set; }
}
