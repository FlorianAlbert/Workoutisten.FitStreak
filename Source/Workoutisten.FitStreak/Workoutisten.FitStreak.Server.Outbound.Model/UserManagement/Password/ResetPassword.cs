using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Password;

[JsonObject("ResetPassword")]
public class ResetPassword
{
    [Required]
    [JsonProperty("PasswordForgottenKey")]
    public string PasswordForgottenKey { get; set; }

    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public string Email { get; set; }

    [Required]
    [JsonProperty("NewPassword")]
    public string NewPassword { get; set; }
}