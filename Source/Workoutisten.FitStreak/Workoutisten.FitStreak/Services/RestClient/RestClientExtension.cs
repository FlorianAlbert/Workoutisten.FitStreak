using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using Workoutisten.FitStreak.Data.Models.User;

namespace Workoutisten.FitStreak.Client.RestClient
{
    public partial class RestClient
    {

        public CustomAuthenticationStateProvider AuthenticationStateProvider { get; set; }


        public RestClient(string baseUrl, System.Net.Http.HttpClient httpClient, CustomAuthenticationStateProvider customAuthenticationStateProvider) : this(baseUrl, httpClient)
        {
            AuthenticationStateProvider = customAuthenticationStateProvider;
        }

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            var accountToken = SecureStorage.GetAsync("accounttoken");
            if (accountToken.Result is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accountToken.Result);
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
                    var accountToken = await SecureStorage.GetAsync("accounttoken");
                    var refreshToken = await SecureStorage.GetAsync("refreshtoken");

                    if (accountToken is not null && refreshToken is not null)
                    {
                        await AuthenticationStateProvider.Login(
                            ConvertRefreshResponseToAuthenticationResponse(
                            await RefreshTokensAsync(
                            new TokenRefreshRequest()
                            {
                                ExpiredJwt = accountToken,
                                RefreshToken = refreshToken
                            })));
                    }

                }
                catch (ApiException e)
                {
                    if (e.StatusCode == 401)
                    {
                        await AuthenticationStateProvider.Logout();
                    }
                }
            }
        }

        private AuthenticationResponse ConvertRefreshResponseToAuthenticationResponse(TokenRefreshResponse tokenRefreshResponse)
        {
            return new AuthenticationResponse()
            {
                Jwt = tokenRefreshResponse.NewJwt,
                RefreshToken = tokenRefreshResponse.NewRefreshToken
            };
        }

    }
}
