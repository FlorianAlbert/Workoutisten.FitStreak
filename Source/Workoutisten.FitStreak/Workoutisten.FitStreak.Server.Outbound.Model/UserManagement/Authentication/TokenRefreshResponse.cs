using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Authentication;

public class TokenRefreshResponse
{
    [Required]
    [JsonProperty("NewRefreshToken")]
    public string NewRefreshToken { get; set; }

    [Required]
    [JsonProperty("NewJwt")]
    public string NewJwt { get; set; }
}
