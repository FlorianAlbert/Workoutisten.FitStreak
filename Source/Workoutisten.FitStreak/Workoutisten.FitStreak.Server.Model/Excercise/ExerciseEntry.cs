using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Workout;

namespace Workoutisten.FitStreak.Server.Model.Excercise
{
    public class ExerciseEntry : BaseEntity
    {
        public virtual Exercise Exercise { get; set; }

        public virtual WorkoutEntry? WorkoutEntry { get; set; }

        public virtual Workout.Workout? Workout { get; set; }
    }
}
