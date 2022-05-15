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
    [JsonProperty("Requester")]
    public User Requester { get; set; }

    [Required]
    [JsonProperty("Requested")]
    public User Requested { get; set; }
}
