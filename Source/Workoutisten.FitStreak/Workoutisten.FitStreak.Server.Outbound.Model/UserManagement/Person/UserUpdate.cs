using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;

[JsonObject("UserUpdate")]
public class UserUpdate
{
    [JsonProperty("Email")]
    [EmailAddress]
    public string? Email { get; set; }

    [JsonProperty("FirstName")]
    public string? FirstName { get; set; }


    [JsonProperty("LastName")]
    public string? LastName { get; set; }
}
