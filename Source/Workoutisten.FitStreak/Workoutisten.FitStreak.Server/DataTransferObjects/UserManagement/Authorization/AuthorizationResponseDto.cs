namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Authorization;

public class AuthorizationResponseDto
{
    public string Token { get; set; }

    public UserDto User { get; set; } 
}