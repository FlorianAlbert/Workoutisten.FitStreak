using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;

[JsonObject("CardioSet")]
public class CardioSet : Set
{
    [Required]
    [JsonProperty("Distance")]
    public double Distance { get; set; }

    [Required]
    [JsonProperty("Duration")]
    public TimeSpan Duration { get; set; }
}
