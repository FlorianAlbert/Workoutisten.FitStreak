using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Data.Models.User
{
    public class UserModel
    {
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Streak { get; set; }

        public DateTime LastExercise { get; set; }

        public int StreakRecord { get; set; }
    }
}
