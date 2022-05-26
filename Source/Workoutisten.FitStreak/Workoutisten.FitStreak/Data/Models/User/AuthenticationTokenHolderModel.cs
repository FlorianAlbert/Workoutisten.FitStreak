using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Data.Models.User
{
    public sealed class AuthenticationTokenHolderModel
    {
        private static readonly AuthenticationTokenHolderModel instance = new AuthenticationTokenHolderModel();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static AuthenticationTokenHolderModel()
        {
        }

        private AuthenticationTokenHolderModel()
        {
        }

        public static AuthenticationTokenHolderModel Instance
        {
            get
            {
                return instance;
            }
        }

        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }
    }
}
