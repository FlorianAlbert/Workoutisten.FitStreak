using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IFriendshipService
{
    Task<bool> AcceptFriendshipRequest(Guid friendshipRequestId);

    Task<bool> DeclineFriendshipRequest(Guid friendshipRequestId);

    Task<bool> CreateFriendshipRequest(Guid requesterId, Guid requestedId);

    Task<bool> WithdrawFriendshipRequest(Guid friendshipRequestId);

    Task<IEnumerable<User>> GetFriends(Guid userId);

    Task<IEnumerable<FriendshipRequest>> GetOutgoingFriendshipRequests(Guid userId);

    Task<IEnumerable<FriendshipRequest>> GetIncomingFriendshipRequests(Guid userId);
}
