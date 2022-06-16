using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Registration
{
    [JsonObject("RegistrationRequest")]
    public class RegistrationRequest
    {
        [Required]
        [EmailAddress]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("Password")]
        public string Password { get; set; }

        [Required]
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [JsonProperty("LastName")]
        public string LastName { get; set; }
    }
}
