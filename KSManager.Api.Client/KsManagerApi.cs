using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KSManager.Api.Client.Helper;
using KSManager.Api.Client.Model;
using KSManager_API.Dto;
using Newtonsoft.Json;

namespace KSManager.Api
{
    public class KsManagerApi : IKsManagerApi
    {
        private const string BaseUri = "http://localhost:51427/api/";

        private readonly HttpClient _httpClient;

        private string _accessToken;

        public KsManagerApi()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseUri);
        }

        public string AccessToken => _accessToken;

        public async Task Authenticate(string username, string password)
        {
            await Authenticate(username, password, CancellationToken.None);
        }

        public async Task Authenticate(string username, string password, CancellationToken cancellationToken)
        {
            var uri = "login/" + "?username=" + Uri.EscapeDataString(username) + "&password=" +
                      Uri.EscapeDataString(password);

            var json = await _httpClient.GetJsonAsync(uri, cancellationToken);

            if (json.StatusCode != System.Net.HttpStatusCode.OK)
                throw new KsManagerApiException("Authentication failed");

            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(json.Json);

            _accessToken = loginResponse.AccessToken;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
        }

        public async Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall()
        {
            return await GetPasswordEntriesSmall(CancellationToken.None);
        }

        public async Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall(CancellationToken cancellationToken)
        {
            const string uri = "passwords/small/";

            var json = await _httpClient.GetJsonAsync(uri, cancellationToken);

            if (json.StatusCode != System.Net.HttpStatusCode.OK)
                throw new KsManagerApiException("Request failed");

            return JsonConvert.DeserializeObject<IEnumerable<SmallPasswordEntry>>(json.Json);
        }
    }
}
