using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KSManager.Api.Client.Helper;
using KSManager.Api.Client.Model;
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

            try
            {
                var json = await _httpClient.GetJsonAsync(uri, cancellationToken);

                if (json.StatusCode != HttpStatusCode.OK)
                    throw new KsManagerApiException("Authentication failed");

                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(json.Json);

                _accessToken = loginResponse.AccessToken;
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_accessToken}");
            }
            catch (Exception ex)
            {
                throw new KsManagerApiException("Reqeust failed", ex);
            }
        }

        public async Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall()
        {
            return await GetPasswordEntriesSmall(CancellationToken.None);
        }

        public async Task<IEnumerable<SmallPasswordEntry>> GetPasswordEntriesSmall(CancellationToken cancellationToken)
        {
            const string uri = "passwords/small/";

            try
            {
                var json = await _httpClient.GetJsonAsync(uri, cancellationToken);

                if (json.StatusCode != HttpStatusCode.OK)
                    throw new KsManagerApiException("Request failed");

                return JsonConvert.DeserializeObject<IEnumerable<SmallPasswordEntry>>(json.Json);
            }
            catch (Exception ex)
            {
                throw new KsManagerApiException("Reqeust failed", ex);
            }
        }

        public Task<PasswordEntry> GetPasswordEntry(Guid id)
        {
            return GetPasswordEntry(id, CancellationToken.None);
        }

        public async Task<PasswordEntry> GetPasswordEntry(Guid id, CancellationToken cancellationToken)
        {
            string uri = "passwords/" + id;

            try
            {
                var json = await _httpClient.GetJsonAsync(uri, cancellationToken);
                if (json.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new KsManagerApiException("Request failed");

                return JsonConvert.DeserializeObject<PasswordEntry>(json.Json);
            }
            catch (Exception ex)
            {
                throw new KsManagerApiException("Request failed", ex);
            }
        }

        public Task UpdatePasswordEntry(PasswordEntry entry)
        {
            return UpdatePasswordEntry(entry, CancellationToken.None);
        }

        public async Task UpdatePasswordEntry(PasswordEntry entry, CancellationToken cancellationToken)
        {
            string uri = "passwords/";
            try
            {
                var json = JsonConvert.SerializeObject(entry);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                await _httpClient.PutAsync(uri, stringContent, cancellationToken);
             
            }
            catch (Exception ex)
            {
                throw new KsManagerApiException("Request failed", ex);
            }
        }
    }
}
