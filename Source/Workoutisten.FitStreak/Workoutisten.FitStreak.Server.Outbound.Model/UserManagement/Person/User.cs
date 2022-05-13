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
    [JsonProperty("Email")]
    public string Email { get; set; }

    [Required]
    [JsonProperty("FirstName")]
    public string FirstName { get; set; }

    [Required]
    [JsonProperty("LastName")]
    public string LastName { get; set; }

    [Required]
    [JsonProperty("CreatedDate")]
    public DateTime CreatedDate { get; set; }

    [Required]
    [JsonProperty("ExerciseStreak")]
    public int ExerciseStreak { get; set; }

    [Required]
    [JsonProperty("LastExercise")]
    public DateTime LastExercise { get; set; }
}