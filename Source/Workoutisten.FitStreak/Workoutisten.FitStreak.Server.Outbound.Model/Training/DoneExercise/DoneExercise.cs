using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;

[JsonObject("DoneCardioExercise")]
public class DoneExercise
{
    [Required]
    [JsonProperty("DoneExerciseId")]
    public Guid DoneExerciseId { get; set; }

    [JsonProperty("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [Required]
    [JsonProperty("ExerciseId")]
    public Guid ExerciseId { get; set; }

    [JsonProperty("ExerciseGroupId")]
    public Guid? ExerciseGroupId { get; set; }

    private IEnumerable<Guid> _SetIds;
    [JsonProperty("SetIds")]
    public IEnumerable<Guid> SetIds
    {
        get => _SetIds ??= new List<Guid>();
        set => _SetIds = value;
    }
}
