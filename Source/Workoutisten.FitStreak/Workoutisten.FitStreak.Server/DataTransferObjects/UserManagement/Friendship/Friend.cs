namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Friendship;
public class Friend
{
    public Guid FriendId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public int ExerciseStreak { get; set; }
}
