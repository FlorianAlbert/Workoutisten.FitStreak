using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Workoutisten.FitStreak.Client.RestClient
{
    public static class RestClientExtensions
    {

        static int counter = 0;

        public static async Task<T> CallControlled<T>(this IRestClient restClient, Func<IRestClient, Task<T>> endpointCall)
        {
            T result = default(T);
            try
            {
                result = await endpointCall(restClient);
            }
            catch (ApiException e)
            {
                if (e.StatusCode == 401 && counter < 2)
                {
                    counter++;
                    result = await restClient.CallControlled(endpointCall);
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                counter = 0;
            }
            return result;
        }

        public static async Task CallControlled(this IRestClient restClient, Func<IRestClient, Task> endpointCall)
        {
            try
            {
                await endpointCall(restClient);
            }
            catch (ApiException e)
            {
                if (e.StatusCode == 401 && counter < 2)
                {
                    counter++;
                    await restClient.CallControlled(endpointCall);
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                counter = 0;
            }
        }
    }
}
