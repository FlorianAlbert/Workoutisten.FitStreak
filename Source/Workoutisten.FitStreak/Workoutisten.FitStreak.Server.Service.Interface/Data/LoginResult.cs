using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Service.Interface.Data;
public class LoginResult
{
    public string RefreshToken { get; set; }

    public string Jwt { get; set; }

    public User User { get; set; }

    public ResultStatus Status { get; set; }
}
