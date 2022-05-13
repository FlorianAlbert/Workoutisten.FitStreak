namespace Workoutisten.FitStreak.Server.Outbound.Model.Training.Group;
public class ExerciseGroup
{
    public Guid ExerciseGroupId { get; set; }

    public DateTime CreationDate { get; set; }

    public string GroupName { get; set; }

    public Guid[] DoneExerciseIds { get; set; }
}
