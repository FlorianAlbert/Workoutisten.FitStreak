namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public class WorkoutModel
    {
        public Guid Guid { get; set; }
        public string WorkoutName { get; set; }

        public string Description { get; set; }

        public DateOnly LastTraining { get; set; }

        //public List<Guid> Exercises { get; set; } = new List<Guid>();

        public List<ExerciseModel> Exercises { get; set; } = new List<ExerciseModel>();
    }
}
