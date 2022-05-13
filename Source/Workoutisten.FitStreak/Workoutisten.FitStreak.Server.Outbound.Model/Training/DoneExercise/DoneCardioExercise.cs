namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;
public class DoneCardioExercise
{
    public Guid DoneExerciseId { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid ExerciseId { get; set; }

    public Guid? WorkoutId { get; set; }

    public Guid? ExerciseGroupId { get; set; }

    public double Distance { get; set; }

    public TimeSpan Duration { get; set; }
}
