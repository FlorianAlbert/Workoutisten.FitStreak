namespace Workoutisten.FitStreak.Server.Model.Account
{
    public class FriendshipRequest : BaseEntity
    {
        public Guid RequestingUserId { get; set; }
        public virtual User RequestingUser { get; set; }

        public Guid RequestedUserId { get; set; }
        public virtual User RequestedUser { get; set; }
    }
}
