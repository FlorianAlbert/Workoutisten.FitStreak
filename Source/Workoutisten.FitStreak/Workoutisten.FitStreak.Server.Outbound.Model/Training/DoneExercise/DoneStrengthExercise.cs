namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;
public class DoneStrengthExercise
{
    public Guid DoneExerciseId { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid ExerciseId { get; set; }

    public Guid? WorkoutId { get; set; }

    public Guid? ExerciseGroupId { get; set; }

    public Dictionary<int, Set> Sets { get; set; }
}
