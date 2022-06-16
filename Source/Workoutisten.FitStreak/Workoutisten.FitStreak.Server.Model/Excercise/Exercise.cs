using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Workout;

namespace Workoutisten.FitStreak.Server.Model.Excercise
{
    public class Exercise : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual User Creator { get; set; }

        public ExerciseCategory Category { get; set; }

        private ICollection<DoneExercise> _ExerciseEntries;
        public virtual ICollection<DoneExercise> ExerciseEntries => _ExerciseEntries ??= new List<DoneExercise>();

        private ICollection<WorkoutExercise> _WorkoutExercises;
        public virtual ICollection<WorkoutExercise> WorkoutExercises => _WorkoutExercises ??= new List<WorkoutExercise>();
    }
}
