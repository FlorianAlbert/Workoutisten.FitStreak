using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Model.Workout
{
    public class ExerciseGroup : BaseEntity
    {
        public string Name { get; set; }

        private ICollection<DoneExercise> _DoneExercises;
        public virtual ICollection<DoneExercise> DoneExercises => _DoneExercises ??= new List<DoneExercise>();

        public virtual Workout? Workout { get; set; }

        private ICollection<User> _Participants;
        public virtual ICollection<User> Participants => _Participants ??= new List<User>();
    }
}
