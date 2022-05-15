using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class FriendshipService : IFriendshipService
{
    public Task<bool> AcceptFriendshipRequest(Guid friendshipRequestId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateFriendshipRequest(Guid requesterId, Guid requestedId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeclineFriendshipRequest(Guid friendshipRequestId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetFriends(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FriendshipRequest>> GetIncomingFriendshipRequests(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FriendshipRequest>> GetOutgoingFriendshipRequests(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> WithdrawFriendshipRequest(Guid friendshipRequestId)
    {
        throw new NotImplementedException();
    }
}
