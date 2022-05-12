using Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Person;

namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Friendship;
public class FriendshipRequest
{
    public Guid FriendshipRequestId { get; set; }

    public User Requester { get; set; }

    public User Requested { get; set; }
}
