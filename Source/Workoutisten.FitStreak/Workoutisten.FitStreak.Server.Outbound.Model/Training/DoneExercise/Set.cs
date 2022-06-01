using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise
{
    [JsonObject("Set")]
    public abstract class Set
    {

        [Required]
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [Required]
        [JsonProperty("DoneExerciseId")]
        public Guid DoneExerciseId { get; set; }
    }
}