using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Authentication;

[JsonObject("AuthenticationResponse")]
public class AuthenticationResponse
{
    [Required]
    [JsonProperty("Token")]
    public string Token { get; set; }


    [Required]
    [JsonProperty("User")]
    public User User { get; set; } 
}