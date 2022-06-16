using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Data.Models.User
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }

        public string VerificationCode { get; set; }

        public string NewPassword { get; set; }
    }
}
