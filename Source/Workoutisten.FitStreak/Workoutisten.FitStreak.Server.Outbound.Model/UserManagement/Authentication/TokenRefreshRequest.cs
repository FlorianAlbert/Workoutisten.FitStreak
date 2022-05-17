using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Authentication;

public class TokenRefreshRequest
{
    [Required]
    [JsonProperty("RefreshToken")]
    public string RefreshToken { get; set; }

    [Required]
    [JsonProperty("ExpiredJwt")]
    public string ExpiredJwt { get; set; }
}
