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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KSManager.Api
{
    public class KsManagerApi : IKsManagerApi
    {
        private const string BaseUri = "http://localhost:51427/api/";

        private static HttpClient _httpClient;

        private string _accessToken;

        public KsManagerApi()
        {
            _httpClient = new HttpClient {BaseAddress = new Uri(BaseUri)};
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

        public Task<PasswordEntry> SavePasswordEntry(PasswordEntry entry)
        {
            return SavePasswordEntry(entry, CancellationToken.None);
        }

        public async Task<PasswordEntry> SavePasswordEntry(PasswordEntry entry, CancellationToken cancellationToken)
        {
            string uri = "passwords/";
            try
            {
                var requestJson = JsonConvert.SerializeObject(entry);

                if (entry.Id == Guid.Empty)
                {
                    var postResponse = await _httpClient.PostJsonAsync(uri, requestJson, cancellationToken);
                    if (postResponse.StatusCode != HttpStatusCode.OK)
                        throw new KsManagerApiException("Request failed");

                    var updatedEntry = JsonConvert.DeserializeObject<PasswordEntry>(postResponse.Json);
                    return updatedEntry;
                }
                else
                {
                    var putResponse = await _httpClient.PutJsonAsync(uri, requestJson, cancellationToken);
                    if (putResponse.StatusCode != HttpStatusCode.OK)
                        throw new KsManagerApiException("Request failed");

                    var updatedEntry = JsonConvert.DeserializeObject<PasswordEntry>(putResponse.Json);
                    return updatedEntry;
                }
            }
            catch (Exception ex)
            {
                throw new KsManagerApiException("Request failed", ex);
            }
        }

        public Task DeletePasswordEntry(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePasswordEntry(Guid id, CancellationToken cancellationToken)
        {
            string uri = "passwords/" + id;
            try
            {
                if (id == Guid.Empty)
                    throw new KsManagerApiException("Request failed");
                await _httpClient.DeleteJsonAsync(uri, CancellationToken.None);

            }
            catch (Exception ex)
            {
                throw new KsManagerApiException("Request failed", ex);
            }
        }

        public Task<RegisterObject> Register(RegisterObject registerObject)
        {
            return Register(registerObject, CancellationToken.None);
        }

        public async Task<RegisterObject> Register(RegisterObject registerObject, CancellationToken cancellationToken)
        {
            string uri = "login";
            try
            {
                var json = JsonConvert.SerializeObject(registerObject);

                var response = await _httpClient.PostJsonAsync(uri, json, cancellationToken);


                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var type = new {message = string.Empty};
                    var message = JsonConvert.DeserializeAnonymousType(response.Json, type);
                    throw new KsManagerApiException(message.message);
                }


                var registered = JsonConvert.DeserializeObject<RegisterObject>(response.Json);
                return registered;
            }
            catch (KsManagerApiException)
                {
                throw;
            }
            catch (Exception ex)
            {
                throw new KsManagerApiException(ex.Message, ex);
            }


        }

        public void ClearProperties()
        {
            _httpClient = new HttpClient{BaseAddress = new Uri(BaseUri)};
            _accessToken = null;
        }
    }
}
