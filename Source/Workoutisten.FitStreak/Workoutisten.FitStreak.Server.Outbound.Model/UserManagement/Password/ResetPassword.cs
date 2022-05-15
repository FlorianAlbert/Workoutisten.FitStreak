using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Password;

[JsonObject("ResetPassword")]
public class ResetPassword
{
    [Required]
    [JsonProperty("NewPassword")]
    public string NewPassword { get; set; }

    [Required]
    [JsonProperty("Token")]
    public Guid Token { get; set; }
}