using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Friendship;

[JsonObject("FriendRequest")]
public class FriendRequest
{
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public string Email { get; set; }
}
