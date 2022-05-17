using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.Workout;

[JsonObject("Workout")]
public class Workout
{
    [Required]
    [JsonProperty("WorkoutId")]
    public Guid WorkoutId { get; set; }

    [JsonProperty("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [Required]
    [JsonProperty("WorkoutName")]
    public string WorkoutName { get; set; }

    [JsonProperty("ExerciseIds")]
    public Guid[] ExerciseIds { get; set; }

    [JsonProperty("DoneExerciseIds")]
    public Guid[] DoneExerciseIds { get; set; }
}