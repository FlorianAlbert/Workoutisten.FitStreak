using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.Exercise;

[JsonObject("Exercise")]
public class Exercise
{
    [Required]
    [JsonProperty("ExerciseId")]
    public Guid ExerciseId { get; set; }

    [Required]
    [JsonProperty("CreationDate")]
    public DateTime CreationDate { get; set; }

    [Required]
    [JsonProperty("Name")]
    public string Name { get; set; }

    [Required]
    [JsonProperty("Description")]
    public string Description { get; set; }

    [Required]
    [JsonProperty("ExerciseCategory")]
    public ExerciseCategory ExerciseCategory { get; set; }

    [Required]
    [JsonProperty("WorkoutIds")]
    public Guid[] WorkoutIds { get; set; }

    [Required]
    [JsonProperty("DoneExerciseIds")]
    public Guid[] DoneExerciseIds { get; set; }
}
