using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Friendship;

[JsonObject("FriendshipRequest")]
public class FriendshipRequest
{
    [Required]
    [JsonProperty("FriendshipRequestId")]
    public Guid FriendshipRequestId { get; set; }

    [Required]
    [JsonProperty("RequestingUser")]
    public User RequestingUser { get; set; }

    [Required]
    [JsonProperty("RequestedUser")]
    public User RequestedUser { get; set; }
}
