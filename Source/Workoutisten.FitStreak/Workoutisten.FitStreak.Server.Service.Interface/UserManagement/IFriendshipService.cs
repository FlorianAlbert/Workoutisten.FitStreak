using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IFriendshipService
{
    Task<Result<bool>> AcceptFriendshipRequest(Guid friendshipRequestId);

    Task<Result<bool>> DeclineFriendshipRequest(Guid friendshipRequestId);

    Task<Result<bool>> CreateFriendshipRequest(Guid requesterId, Guid requestedId);

    Task<bool> WithdrawFriendshipRequest(Guid friendshipRequestId);

    Task<Result<IEnumerable<User>>> GetFriends(Guid userId);

    Task<Result<IEnumerable<FriendshipRequest>>> GetOutgoingFriendshipRequests(Guid userId);

    Task<Result<IEnumerable<FriendshipRequest>>> GetIncomingFriendshipRequests(Guid userId);
}
