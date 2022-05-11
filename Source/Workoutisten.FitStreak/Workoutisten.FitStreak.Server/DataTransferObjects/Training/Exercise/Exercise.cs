namespace Workoutisten.FitStreak.Server.DataTransferObjects.Training.Exercise;
public class Exercise
{
    public Guid ExerciseId { get; set; }

    public DateTime CreationDate { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ExerciseCategory ExerciseCategory { get; set; }

    public Guid[] WorkoutIds { get; set; }

    public Guid[] DoneExerciseIds { get; set; }
}
