using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Model.Account
{
    public class User : BaseEntity
    {
        public string NormalizedEmail { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsVerified { get; set; }

        public Guid? PasswordForgottenKey { get; set; }

        private ICollection<Exercise> _Exercises;
        public virtual ICollection<Exercise> Exercises => _Exercises ??= new List<Exercise>();

        private ICollection<Workout.Workout> _Workouts;
        public virtual ICollection<Workout.Workout> Workouts => _Workouts ??= new List<Workout.Workout>();
    }
}
