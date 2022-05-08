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
    }
}
