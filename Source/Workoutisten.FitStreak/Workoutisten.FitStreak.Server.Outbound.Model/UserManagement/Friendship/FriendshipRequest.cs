using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Friendship;
public class FriendshipRequest
{
    public Guid FriendshipRequestId { get; set; }

    public User Requester { get; set; }

    public User Requested { get; set; }
}
