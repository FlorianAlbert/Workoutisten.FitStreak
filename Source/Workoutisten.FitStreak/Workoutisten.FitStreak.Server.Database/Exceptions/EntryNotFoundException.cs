namespace Workoutisten.FitStreak.Server.Database.Implementation.Exceptions
{
    public class EntryNotFoundException : Exception
    {
        public Type EntryType { get; set; }

        public Guid Key { get; set; }

        public EntryNotFoundException(Guid key, Type type) : base()
        {
            Key = key;
            EntryType = type;
        }

        public EntryNotFoundException(Guid key, Type type, string? message) : base(message)
        {
            Key = key;
            EntryType = type;
        }

        public EntryNotFoundException(Guid key, Type type, string? message, Exception? innerException) : base(message, innerException)
        {
            Key = key;
            EntryType = type;
        }
    }
}
