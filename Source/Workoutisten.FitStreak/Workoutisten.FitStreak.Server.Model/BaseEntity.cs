namespace Workoutisten.FitStreak.Server.Model
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}