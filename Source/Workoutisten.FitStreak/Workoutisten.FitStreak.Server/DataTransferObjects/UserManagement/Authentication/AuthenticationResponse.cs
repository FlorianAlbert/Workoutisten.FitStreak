namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Authentication;

public class AuthenticationResponse
{
    public string Token { get; set; }

    public User User { get; set; } 
}