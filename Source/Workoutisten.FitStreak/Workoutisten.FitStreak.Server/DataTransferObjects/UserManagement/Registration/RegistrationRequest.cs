namespace Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Registration
{
    public class RegistrationRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}
