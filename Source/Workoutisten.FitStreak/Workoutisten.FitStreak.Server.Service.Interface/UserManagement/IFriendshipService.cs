using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IFriendshipService
{
    Task<Result> AcceptFriendshipRequestAsync(Guid requestedUserId, Guid friendshipRequestId);

    Task<Result> CreateFriendshipRequestAsync(Guid requestingUserId, string requestedUserEmail);

    Task<Result> DeclineFriendshipRequestAsync(Guid requestedUserId, Guid friendshipRequestId);

    Task<Result> DeleteFollowerAsync(Guid userId, Guid followerId);

    Task<Result<IEnumerable<User>>> GetFollowedUsersAsync(Guid userId);

    Task<Result<IEnumerable<User>>> GetFollowerAsync(Guid userId);

    Task<Result<IEnumerable<FriendshipRequest>>> GetIncomingFriendshipRequestsAsync(Guid userId);

    Task<Result<IEnumerable<FriendshipRequest>>> GetOutgoingFriendshipRequestsAsync(Guid userId);

    Task<Result> UnfollowUserAsync(Guid userId, Guid followedUserId);

    Task<Result> WithdrawFriendshipRequestAsync(Guid requestingUserId, Guid friendshipRequestId);
}
