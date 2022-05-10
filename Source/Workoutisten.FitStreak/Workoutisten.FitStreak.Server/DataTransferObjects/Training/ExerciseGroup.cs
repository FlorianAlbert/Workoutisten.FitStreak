namespace Workoutisten.FitStreak.Server.DataTransferObjects.Training
{
    public class ExerciseGroup
    {
        public Guid ExerciseGroupId { get; set; }

        public DateTime CreationDate { get; set; }

        public string GroupName { get; set; }

        public Guid[] DoneExerciseIds { get; set; }
    }
}
