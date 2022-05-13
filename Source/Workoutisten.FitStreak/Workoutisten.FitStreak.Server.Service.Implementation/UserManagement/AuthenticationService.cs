using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class AuthenticationService : IAuthenticationService
{
    public async Task<LoginResult> LoginAsync(string email, string password)
    {
        //ToDo implementieren und User-Model anpassen -> siehe Controller
        return new LoginResult
        {
            Status = LoginResultStatus.BadRequest
        };
    }
}
