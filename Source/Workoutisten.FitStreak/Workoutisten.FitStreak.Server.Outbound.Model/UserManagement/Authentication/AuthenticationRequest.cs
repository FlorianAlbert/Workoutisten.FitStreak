using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Authentication;

[JsonObject("AuthenticationRequest")]
public class AuthenticationRequest
{
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public string Email { get; set; }

    [Required]
    [JsonProperty("Password")]
    public string Password { get; set; }
}