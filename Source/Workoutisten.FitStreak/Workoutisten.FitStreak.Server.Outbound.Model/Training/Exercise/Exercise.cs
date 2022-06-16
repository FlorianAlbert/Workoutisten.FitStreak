using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.Exercise;

[JsonObject("Exercise")]
public class Exercise
{
    [Required]
    [JsonProperty("Name")]
    public string Name { get; set; }

    [Required]
    [JsonProperty("Description")]
    public string Description { get; set; }

    [Required]
    [JsonProperty("ExerciseCategory")]
    public ExerciseCategory ExerciseCategory { get; set; }

    [JsonProperty("ExerciseId")]
    public Guid? ExerciseId { get; set; }

    [JsonProperty("CreatedAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonProperty("CreatorId")]
    public Guid? CreatorId { get; set; }

    [JsonProperty("WorkoutIds")]
    public Guid[]? WorkoutIds { get; set; }

    [JsonProperty("DoneExerciseIds")]
    public Guid[]? DoneExerciseIds { get; set; }
}
