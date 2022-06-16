using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Workoutisten.FitStreak.Data.Models.User;
using Microsoft.AspNetCore.Components;
using Workoutisten.FitStreak.Client.RestClient;
using System.Text.Json;

namespace Workoutisten.FitStreak
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
       

        public CustomAuthenticationStateProvider()
        {
            
        }

        /// <summary>
        /// This method should be called upon a successful user login, and it will store the user's JWT token in SecureStorage.
        /// Upon saving this it will also notify .NET that the authentication state has changed which will enable authenticated views
        /// </summary>
        /// <param name="token">Our JWT to store</param>
        /// <returns></returns>
        public async Task Login(AuthenticationResponse response)
        {
            if(response == null) throw new ArgumentNullException(nameof(response));

            //maybe do/store/save anything as part of this process
            await SecureStorage.SetAsync("accounttoken", response.Jwt);
            await SecureStorage.SetAsync("refreshtoken", response.RefreshToken);
            await SecureStorage.SetAsync("userId", response.User.UserId.ToString());
            await SecureStorage.SetAsync("userName", response.User.FirstName);

            //Providing the current identity ifnormation
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        

        public async Task RefreshTokens(TokenRefreshResponse response)
        {
            await SecureStorage.SetAsync("accounttoken", response.NewJwt);
            await SecureStorage.SetAsync("refreshtoken", response.NewRefreshToken);
        }

        /// <summary>
        /// This method should be called to log-off the user from the application, which simply removed the stored token and then
        /// notifies of the change
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            SecureStorage.Remove("accounttoken");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        /// <summary>
        /// This is the key method that is called by .NET to accomplish our goal.  It is the method that is queried to get the current 
        /// AuthenticationState for the user.  In our base, if we have a token in secure storage, we are logged in, but we could easily
        /// do much more than this. 
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userInfo = await SecureStorage.GetAsync("accounttoken");
                var userName = await SecureStorage.GetAsync("userName");
                if (userInfo != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, userName)};
                    var identity = new ClaimsIdentity(claims, "Custom authentication");
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }
            }
            catch (Exception ex)
            {
                //This should be more properly handled
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        //public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        //{
        //    var payload = jwt.Split('.')[1];
        //    var jsonBytes = ParseBase64WithoutPadding(payload);
        //    var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        //    return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        //}

        //private static byte[] ParseBase64WithoutPadding(string base64)
        //{
        //    switch (base64.Length % 4)
        //    {
        //        case 2: base64 += "=="; break;
        //        case 3: base64 += "="; break;
        //    }
        //    return Convert.FromBase64String(base64);
        //}

    }
}
