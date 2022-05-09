namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Authentication;

public class AuthenticationResponseDto
{
    public string Token { get; set; }

    public UserDto User { get; set; } 
}