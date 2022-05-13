using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IFriendshipService : IBaseEntityService<FriendshipRequest>
{
    Task<bool> AcceptFriendshipRequest(Guid friendshipRequestId);

    Task<bool> DeclineFriendshipRequest(Guid friendshipRequestId);
}
