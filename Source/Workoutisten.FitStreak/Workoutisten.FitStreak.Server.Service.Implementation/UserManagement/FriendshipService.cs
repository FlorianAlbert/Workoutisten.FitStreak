using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class FriendshipService : IFriendshipService
{
    public async Task<FriendshipRequest> CreateOrUpdateAsync(FriendshipRequest entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(FriendshipRequest entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<FriendshipRequest>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<FriendshipRequest>> GetAllForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<FriendshipRequest> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
