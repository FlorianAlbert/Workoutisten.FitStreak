using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Model.Workout
{
    public class WorkoutExercise : BaseEntity
    {
        public Guid WorkoutId { get; set; }
        public virtual Workout Workout { get; set; }

        public Guid ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
    }
}
