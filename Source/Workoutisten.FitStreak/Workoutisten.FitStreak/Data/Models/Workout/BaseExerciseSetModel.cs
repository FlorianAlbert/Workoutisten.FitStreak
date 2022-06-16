namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public abstract class BaseExerciseSetModel
    {
        public int SetNumber { get; set; }

        public string Name { get { return $"Set {SetNumber + 1}"; } }

        public Guid SetId { get; set; }

        public Guid DoneExerciseId { get; set; }
    }
}
