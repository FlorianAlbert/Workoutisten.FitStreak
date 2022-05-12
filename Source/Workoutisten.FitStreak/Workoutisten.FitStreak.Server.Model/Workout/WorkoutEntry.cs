using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Model.Workout
{
    public class WorkoutEntry : BaseEntity
    {
        private ICollection<ExerciseEntry> _ExerciseEntries;
        public virtual ICollection<ExerciseEntry> ExerciseEntries => _ExerciseEntries ??= new List<ExerciseEntry>();
    }
}
