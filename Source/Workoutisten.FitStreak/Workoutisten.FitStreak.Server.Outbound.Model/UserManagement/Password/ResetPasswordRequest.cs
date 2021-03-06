using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Password;

[JsonObject("ResetPasswordRequest")]
public class ResetPasswordRequest
{
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public string Email { get; set; }
}