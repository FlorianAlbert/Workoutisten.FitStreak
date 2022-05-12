namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Friendship;
public class FriendshipRequest
{
    public Guid FriendshipRequestId { get; set; }

    public Friend Requester { get; set; }
}
