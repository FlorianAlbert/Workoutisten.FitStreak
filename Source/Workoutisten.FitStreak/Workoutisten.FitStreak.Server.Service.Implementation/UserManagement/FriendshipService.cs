using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class FriendshipService : IFriendshipService
{
    public Task<Result<bool>> AcceptFriendshipRequest(Guid friendshipRequestId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> CreateFriendshipRequest(Guid requesterId, Guid requestedId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> DeclineFriendshipRequest(Guid friendshipRequestId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<User>>> GetFriends(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<FriendshipRequest>>> GetIncomingFriendshipRequests(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<FriendshipRequest>>> GetOutgoingFriendshipRequests(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> WithdrawFriendshipRequest(Guid friendshipRequestId)
    {
        throw new NotImplementedException();
    }
}
