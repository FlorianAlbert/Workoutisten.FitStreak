﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;

[JsonObject("DoneCardioExercise")]
public class DoneStrengthExercise
{
    [Required]
    [JsonProperty("DoneExerciseId")]
    public Guid DoneExerciseId { get; set; }

    [JsonProperty("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [Required]
    [JsonProperty("ExerciseId")]
    public Guid ExerciseId { get; set; }

    [JsonProperty("WorkoutId")]
    public Guid? WorkoutId { get; set; }

    [JsonProperty("ExerciseGroupId")]
    public Guid? ExerciseGroupId { get; set; }

    [Required]
    [JsonProperty("Sets")]
    public Dictionary<int, Set> Sets { get; set; }
}