using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KSManager.Api.Client.Helper
{
    internal static class HttpClientExtensions
    {
        public static async Task<JsonResponse> GetJsonAsync(this HttpClient httpClient, string uri, CancellationToken cancellationToken)
        {
            using (var response = await httpClient.GetAsync(uri, cancellationToken))
            {
                var json = await response.Content.ReadAsStringAsync();

                return new JsonResponse
                {
                    Json = json,
                    StatusCode = response.StatusCode
                };
            }
        }
    }
}
