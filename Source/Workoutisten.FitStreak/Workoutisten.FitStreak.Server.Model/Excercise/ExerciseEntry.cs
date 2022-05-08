namespace Workoutisten.FitStreak.Server.Model.Excercise
{
    public class ExerciseEntry : BaseEntity
    {
        public Guid ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public Guid WorkoutId { get; set; }
        public virtual Workout.Workout Workout { get; set; }

        public int OrderNumber { get; set; }
    }
}
