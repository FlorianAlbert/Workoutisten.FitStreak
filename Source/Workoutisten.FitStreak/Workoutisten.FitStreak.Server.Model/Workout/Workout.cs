using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Model.Workout
{
    public class Workout : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual User Creator { get; set; }

        private ICollection<ExerciseEntry> _ExerciseEntries;
        public virtual ICollection<ExerciseEntry> ExerciseEntries => _ExerciseEntries ??= new List<ExerciseEntry>();
    }
}
