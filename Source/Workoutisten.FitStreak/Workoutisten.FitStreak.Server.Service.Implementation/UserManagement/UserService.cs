using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class UserService : IUserService
{
    public async Task<int> HasDoneExerciseAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
