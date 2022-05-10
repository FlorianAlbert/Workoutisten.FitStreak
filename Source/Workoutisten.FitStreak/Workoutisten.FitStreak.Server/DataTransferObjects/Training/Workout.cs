namespace Workoutisten.FitStreak.Server.DataTransferObjects.Training;

public class Workout
{
    public Guid WorkoutId { get; set; }

    public DateTime CreationDate { get; set; }

    public string WorkoutName { get; set; }

    public Guid[] ExerciseIds { get; set; }

    public Guid[] DoneExerciseIds { get; set; }
}