namespace Workoutisten.FitStreak.Server.Model.Account
{
    public class Email : BaseEntity
    {
        public string Subject { get; set; }

        public string Message { get; set; }

        private ICollection<User> _Receivers;
        public virtual ICollection<User> Receivers => _Receivers ??= new List<User>();
    }
}
