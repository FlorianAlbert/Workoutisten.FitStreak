using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Authentication;

[JsonObject("AuthenticationResponse")]
public class AuthenticationResponse
{
    [Required]
    [JsonProperty("RefreshToken")]
    public string RefreshToken { get; set; }

    [Required]
    [JsonProperty("Jwt")]
    public string Jwt { get; set; }


    [Required]
    [JsonProperty("User")]
    public User User { get; set; } 
}