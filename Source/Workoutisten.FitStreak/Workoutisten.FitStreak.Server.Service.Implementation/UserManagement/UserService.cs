using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class UserService : IUserService
{
    public async Task<User> CreateOrUpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> HasDoneExerciseAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
