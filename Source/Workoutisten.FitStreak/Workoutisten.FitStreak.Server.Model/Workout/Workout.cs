using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Model.Workout
{
    public class Workout : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual User Creator { get; set; }

        private ICollection<WorkoutExercise> _WorkoutExercises;
        public virtual ICollection<WorkoutExercise> WorkoutExercises => _WorkoutExercises ??= new List<WorkoutExercise>();

        private ICollection<ExerciseEntry> _WorkoutContextExerciseEntries;
        public virtual ICollection<ExerciseEntry> WorkoutContextExerciseEntries => _WorkoutContextExerciseEntries ??= new List<ExerciseEntry>();
    }
}
