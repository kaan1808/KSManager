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

        public static async Task<JsonResponse> PutJsonAsync(this HttpClient httpClient, string uri, string content ,CancellationToken cancellationToken )
        {
            
            using (var response = await httpClient.PutAsync(uri, new StringContent(content,Encoding.UTF8,"application/json"), cancellationToken))
            {
                var json = await response.Content.ReadAsStringAsync();
                return new JsonResponse
                {
                    Json = json,
                    StatusCode = response.StatusCode
                };
            }
        }
        public static async Task<JsonResponse> PostJsonAsync(this HttpClient httpClient, string uri, string content, CancellationToken cancellationToken)
        {
            using (var response = await httpClient.PostAsync(uri, new StringContent(content, Encoding.UTF8, "application/json"), cancellationToken))
            {
                var json = await response.Content.ReadAsStringAsync();
                return new JsonResponse
                {
                    Json = json,
                    StatusCode = response.StatusCode
                };
            }
        }

        public static async Task<JsonResponse> DeleteJsonAsync(this HttpClient httpClient, string uri, CancellationToken cancellationToken)
        {
            using (var response = await httpClient.DeleteAsync(uri, cancellationToken))
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
