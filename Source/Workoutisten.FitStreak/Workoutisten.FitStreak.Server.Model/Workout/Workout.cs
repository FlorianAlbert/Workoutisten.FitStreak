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

        private ICollection<ExerciseGroup> _ExerciseGroups;
        public virtual ICollection<ExerciseGroup> ExerciseGroups => _ExerciseGroups ??= new List<ExerciseGroup>();
    }
}
