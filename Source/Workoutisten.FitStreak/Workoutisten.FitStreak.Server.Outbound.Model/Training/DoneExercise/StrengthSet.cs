using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;

[JsonObject("StrengthSet")]
public class StrengthSet : Set
{
    [Required]
    [JsonProperty("Weight")]
    public double Weight { get; set; }

    [Required]
    [JsonProperty("Repetitions")]
    public int Repetitions { get; set; }
}
