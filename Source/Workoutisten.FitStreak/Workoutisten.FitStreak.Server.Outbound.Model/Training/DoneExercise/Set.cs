using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;

[JsonObject("Set")]
public class Set
{
    [Required]
    [JsonProperty("Weigth")]
    public double Weigth { get; set; }

    [Required]
    [JsonProperty("Repetitions")]
    public int Repetitions { get; set; }
}
