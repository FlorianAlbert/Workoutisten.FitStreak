using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;

[JsonObject("DoneCardioExercise")]
public class DoneCardioExercise
{
    [Required]
    [JsonProperty("DoneExerciseId")]
    public Guid DoneExerciseId { get; set; }

    [Required]
    [JsonProperty("CreationDate")]
    public DateTime CreationDate { get; set; }

    [Required]
    [JsonProperty("ExerciseId")]
    public Guid ExerciseId { get; set; }

    [Required]
    [JsonProperty("WorkoutId")]
    public Guid? WorkoutId { get; set; }

    [Required]
    [JsonProperty("ExerciseGroupId")]
    public Guid? ExerciseGroupId { get; set; }

    [Required]
    [JsonProperty("Distance")]
    public double Distance { get; set; }

    [Required]
    [JsonProperty("Duration")]
    public TimeSpan Duration { get; set; }
}
