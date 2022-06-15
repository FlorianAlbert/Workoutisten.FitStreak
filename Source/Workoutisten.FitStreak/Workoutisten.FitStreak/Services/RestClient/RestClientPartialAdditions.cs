using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Workoutisten.FitStreak.Data.Models.User;

namespace Workoutisten.FitStreak.Client.RestClient
{
    public partial class RestClient
    {
        static int refreshCounter = 0;

        public CustomAuthenticationStateProvider AuthenticationStateProvider { get; set; }


        public RestClient(string baseUrl, System.Net.Http.HttpClient httpClient, CustomAuthenticationStateProvider customAuthenticationStateProvider) : this(baseUrl, httpClient)
        {
            AuthenticationStateProvider = customAuthenticationStateProvider;
        }

        async Task PrepareRequestAsync(HttpClient client, HttpRequestMessage request, string url, CancellationToken cancellationToken)
        {
            var accountToken = await SecureStorage.GetAsync("accounttoken");
            if (accountToken is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accountToken);
            }
        }

        async Task PrepareRequestAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder, CancellationToken cancellationToken)
        {
            //PrepareRequestAsync(client, request, urlBuilder.ToString(), cancellationToken);
        }

        async Task ProcessResponseAsync(HttpClient client, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                try
                {
                    var accountToken = await SecureStorage.GetAsync("accounttoken");
                    var refreshToken = await SecureStorage.GetAsync("refreshtoken");

                    if (accountToken is not null && refreshToken is not null)
                    {
                        await AuthenticationStateProvider.RefreshTokens(
                            await RefreshTokensAsync(
                            new TokenRefreshRequest()
                            {
                                ExpiredJwt = accountToken,
                                RefreshToken = refreshToken
                            }));
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

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            //settings.TypeNameHandling = TypeNameHandling.Objects;
        }

    }
}
