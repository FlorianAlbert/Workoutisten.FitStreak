namespace Workoutisten.FitStreak.Server.Database.Implementation.Exceptions
{
    public class DatabaseRepositoryException : Exception
    {
        public string DatabaseName { get; set; }

        public DatabaseRepositoryException(string databaseName) : base()
        {
            DatabaseName = databaseName;
        }

        public DatabaseRepositoryException(string databaseName, string? message) : base(message)
        {
            DatabaseName = databaseName;
        }

        public DatabaseRepositoryException(string databaseName, string? message, Exception? innerException) : base(message, innerException)
        {
            DatabaseName = databaseName;
        }
    }
}
