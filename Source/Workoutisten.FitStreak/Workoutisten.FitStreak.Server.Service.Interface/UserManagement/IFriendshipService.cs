using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IFriendshipService
{
    Task<Result> AcceptFriendshipRequestAsync(Guid userId, Guid friendshipRequestId);

    Task<Result> CreateFriendshipRequestAsync(Guid userId, string requestedUserEmail);

    Task<Result> DeclineFriendshipRequestAsync(Guid userId, Guid friendshipRequestId);

    Task<Result> DeleteFollowerAsync(Guid userId, Guid followerId);

    Task<Result<IEnumerable<User>>> GetFollowedUsersAsync(Guid userId);

    Task<Result<IEnumerable<User>>> GetFollowerAsync(Guid userId);

    Task<Result<IEnumerable<FriendshipRequest>>> GetIncomingFriendshipRequestsAsync(Guid userId);

    Task<Result<IEnumerable<FriendshipRequest>>> GetOutgoingFriendshipRequestsAsync(Guid userId);

    Task<Result> UnfollowUserAsync(Guid userId, Guid followedUserId);

    Task<Result> WithdrawFriendshipRequestAsync(Guid userId, Guid friendshipRequestId);
}
