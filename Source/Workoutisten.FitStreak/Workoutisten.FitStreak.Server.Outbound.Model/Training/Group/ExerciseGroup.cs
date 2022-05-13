using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.Group;

[JsonObject("ExerciseGroup")]
public class ExerciseGroup
{
    [Required]
    [JsonProperty("ExerciseGroupId")]
    public Guid ExerciseGroupId { get; set; }

    [Required]
    [JsonProperty("CreationDate")]
    public DateTime CreationDate { get; set; }

    [Required]
    [JsonProperty("GroupName")]
    public string GroupName { get; set; }

    [Required]
    [JsonProperty("DoneExerciseIds")]
    public Guid[] DoneExerciseIds { get; set; }
}
