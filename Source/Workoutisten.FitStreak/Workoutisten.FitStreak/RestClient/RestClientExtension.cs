using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Client.RestClient
{
    public partial class RestClient
    {
        //[Inject]
        //public TokenHolderWhatever MyProperty { get; set; }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            //To be done
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder)
        {
            PrepareRequest(client, request, urlBuilder.ToString());
        }
    }
}
