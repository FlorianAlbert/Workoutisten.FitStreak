using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Registration
{
    [JsonObject("RegistrationRequest")]
    public class RegistrationRequest
    {
        [Required]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
