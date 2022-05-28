using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using Workoutisten.FitStreak.Data.Models.User;

namespace Workoutisten.FitStreak.Client.RestClient
{
    public partial class RestClient
    {

        [Inject]
        public CustomAuthenticationStateProvider AuthenticationStateProvider { get; set; }


        public RestClient(CustomAuthenticationStateProvider customAuthenticationStateProvider)
        {
            AuthenticationStateProvider = customAuthenticationStateProvider;
        }

        async partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            var accountToken = await SecureStorage.GetAsync("accounttoken");
            if (accountToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accountToken);
            }
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder)
        {
            PrepareRequest(client, request, urlBuilder.ToString());
        }

        async partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                try
                {
                    //await AuthenticationStateProvider.Login(
                    //await RefreshTokensAsync(
                    //    new TokenRefreshRequest()
                    //    {
                    //        ExpiredJwt = await SecureStorage.GetAsync("accounttoken"),
                    //        RefreshToken = await SecureStorage.GetAsync("refreshtoken")
                    //    }));
                }
                catch (ApiException e)
                {
                    await AuthenticationStateProvider.Logout();
                }
            }
        }


    }
}
