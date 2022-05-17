using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.Group;

[JsonObject("ExerciseGroup")]
public class ExerciseGroup
{
    [Required]
    [JsonProperty("ExerciseGroupId")]
    public Guid ExerciseGroupId { get; set; }

    [JsonProperty("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [Required]
    [JsonProperty("GroupName")]
    public string GroupName { get; set; }

    [JsonProperty("DoneExerciseIds")]
    public Guid[] DoneExerciseIds { get; set; }
}
