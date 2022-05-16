using Microsoft.AspNetCore.Components;
using Workoutisten.FitStreak.Data.Models.User;

namespace Workoutisten.FitStreak.Client.RestClient
{
    public partial class RestClient
    {
        [Inject]
        public AuthenticationTokenHolderModel TokenHolder { get; set; }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            request.Headers.Authorization = System.Net.Http.Headers.AuthenticationHeaderValue.Parse(TokenHolder.AccessToken);
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder)
        {
            PrepareRequest(client, request, urlBuilder.ToString());
        }

        partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
        {
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //this.
                //Wie refreshen wir den Token?
            }
        }
    }
}
