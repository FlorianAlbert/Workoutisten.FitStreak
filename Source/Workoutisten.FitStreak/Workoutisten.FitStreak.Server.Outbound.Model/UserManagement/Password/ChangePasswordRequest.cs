using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Password;

[JsonObject("ChangePasswordRequest")]
public class ChangePasswordRequest
{
    [Required]
    [JsonProperty("Email")]
    public string Email { get; set; }

    [Required]
    [JsonProperty("OldPassword")]
    public string OldPassword { get; set; }

    [Required]
    [JsonProperty("NewPassword")]
    public string NewPassword { get; set; }
}