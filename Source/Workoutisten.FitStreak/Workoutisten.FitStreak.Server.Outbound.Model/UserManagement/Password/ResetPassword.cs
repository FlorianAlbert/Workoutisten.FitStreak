namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Password;

public class ResetPassword
{
    public string NewPassword { get; set; }

    public Guid Token { get; set; }
}