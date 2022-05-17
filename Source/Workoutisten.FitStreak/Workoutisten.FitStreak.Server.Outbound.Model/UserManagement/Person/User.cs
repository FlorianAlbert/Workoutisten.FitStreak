using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;

[JsonObject("User")]
public class User
{
    [Required]
    [JsonProperty("UserId")]
    public Guid UserId { get; set; }

    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public string Email { get; set; }

    [Required]
    [JsonProperty("FirstName")]
    public string FirstName { get; set; }

    [Required]
    [JsonProperty("LastName")]
    public string LastName { get; set; }

    [JsonProperty("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("ExerciseStreak")]
    public int ExerciseStreak { get; set; }

    [JsonProperty("LastExercise")]
    public DateTime LastExercise { get; set; }
}