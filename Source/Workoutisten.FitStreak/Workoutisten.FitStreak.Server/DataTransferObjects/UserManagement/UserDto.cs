namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement;

public class UserDto
{
    public Guid UserId { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}