using System.ComponentModel.DataAnnotations.Schema;
using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Model.Workout;

namespace Workoutisten.FitStreak.Server.Model.Account
{
    [Table("User")]
    public class User : BaseEntity
    {
        public string NormalizedEmail { get; set; }

        public string PasswordHash { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? RefreshToken { get; set; }

        public string? RegistrationConfirmationKey { get; set; }

        public string? PasswordForgottenKey { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

        public bool IsVerified { get; set; }

        public int Streak { get; set; }

        public int MaxStreak { get; set; }

        private ICollection<Exercise> _Exercises;
        public virtual ICollection<Exercise> Exercises => _Exercises ??= new List<Exercise>();

        private ICollection<Workout.Workout> _Workouts;
        public virtual ICollection<Workout.Workout> Workouts => _Workouts ??= new List<Workout.Workout>();

        private ICollection<Email> _Emails;
        public virtual ICollection<Email> Emails => _Emails ??= new List<Email>();

        private ICollection<User> _MyFriends;
        public virtual ICollection<User> MyFriends => _MyFriends ??= new List<User>();

        private ICollection<User> _UsersIAmFriendOf;
        public virtual ICollection<User> UsersIAmFriendOf => _UsersIAmFriendOf ??= new List<User>();

        private ICollection<FriendshipRequest> _OutgoingFriendshipRequests;
        public virtual ICollection<FriendshipRequest> OutgoingFriendshipRequests => _OutgoingFriendshipRequests ??= new List<FriendshipRequest>();

        private ICollection<FriendshipRequest> _IngoingFriendshipRequests;
        public virtual ICollection<FriendshipRequest> IngoingFriendshipRequests => _IngoingFriendshipRequests ??= new List<FriendshipRequest>();

        private ICollection<DoneExercise> _DoneExercises;
        public virtual ICollection<DoneExercise> DoneExercises => _DoneExercises ??= new List<DoneExercise>();

        private ICollection<ExerciseGroup> _ParticipatedExerciseGroups;
        public virtual ICollection<ExerciseGroup> ParticipatedExerciseGroups => _ParticipatedExerciseGroups ??= new List<ExerciseGroup>();
    }
}
