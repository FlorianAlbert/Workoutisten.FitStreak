using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Registration;

[JsonObject("RegistrationConfirmation")]
public class RegistrationConfirmation
{
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public string Email { get; set; }

    [Required]
    [JsonProperty("ConfirmationKey")]
    public string ConfirmationKey { get; set; }
}
