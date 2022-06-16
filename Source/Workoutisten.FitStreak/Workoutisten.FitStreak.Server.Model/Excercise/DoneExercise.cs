using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Workout;

namespace Workoutisten.FitStreak.Server.Model.Excercise
{
    public class DoneExercise : BaseEntity
    {
        public virtual Exercise Exercise { get; set; }

        public virtual ExerciseGroup? ExerciseGroup { get; set; }

        private ICollection<Set> _Sets;
        public virtual ICollection<Set> Sets => _Sets ??= new List<Set>();

        public virtual User ExercisingUser { get; set; }
    }
}
