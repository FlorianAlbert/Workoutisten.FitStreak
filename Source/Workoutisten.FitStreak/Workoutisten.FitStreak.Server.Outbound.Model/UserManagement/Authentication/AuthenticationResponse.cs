using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Authentication;

public class AuthenticationResponse
{
    public string Token { get; set; }

    public User User { get; set; } 
}