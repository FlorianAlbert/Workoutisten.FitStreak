using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.Exercise;

[JsonObject("Exercise")]
public class Exercise
{
    [Required]
    [JsonProperty("ExerciseId")]
    public Guid ExerciseId { get; set; }

    [JsonProperty("CreationDate")]
    public DateTime CreationDate { get; set; }

    [Required]
    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Description")]
    public string Description { get; set; }

    [Required]
    [JsonProperty("ExerciseCategory")]
    public ExerciseCategory ExerciseCategory { get; set; }

    [JsonProperty("WorkoutIds")]
    public Guid[] WorkoutIds { get; set; }

    [JsonProperty("DoneExerciseIds")]
    public Guid[] DoneExerciseIds { get; set; }
}
