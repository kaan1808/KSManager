using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KSManager.Model;
using Newtonsoft.Json;

namespace KSManager.Helper
{
    public static class HttpOperationsHelper
    {
        private static HttpClient client;
        private const string RequestUser = "http://localhost:51427/api/users/";
        private const string RequestPasswordStorageData = "http://localhost:51427/api/passwordstoragedatas/";

        static HttpOperationsHelper()
        {
            client = new HttpClient();
        }

        #region Users
        public static async void AddUser(StringContent content)
        {
            await client.PostAsync(RequestUser, content);
        }

        public static async Task<IEnumerable<User>> GetAllUser()
        {
            var response = await client.GetStringAsync(RequestUser);
            var user = JsonConvert.DeserializeObject<IEnumerable<User>>(response);

            return user;
        }

        public static async Task<User> GetUserById(Guid id)
        {
            var response = await client.GetStringAsync(RequestUser + id);
            var user = JsonConvert.DeserializeObject<User>(response);

            return user;
        }

        public static async void DeleteUser(Guid id)
        {
            await client.DeleteAsync(RequestUser + id);
        }

        public static async void UpdateUser(Guid id, StringContent content)
        {
            await client.PutAsync(RequestUser + id, content);
        }
        #endregion Users

        #region PasswordStorageDatas
        public static async void AddEntry(StringContent content)
        {
            await client.PostAsync(RequestPasswordStorageData, content);
        }

        public static async Task<IEnumerable<PasswordStorageData>> GetAllEntries()
        {
            var response = await client.GetStringAsync(RequestPasswordStorageData);
            var entry = JsonConvert.DeserializeObject<IEnumerable<PasswordStorageData>>(response);

            return entry;
        }

        public static async Task<PasswordStorageData> GetEntryById(Guid id)
        {
            var response = await client.GetStringAsync(RequestPasswordStorageData + id);
            var entry = JsonConvert.DeserializeObject<PasswordStorageData>(response);

            return entry;
        }

        public static async void DeleteEntry(Guid id)
        {
            await client.DeleteAsync(RequestPasswordStorageData + id);
        }

        public static async void UpdateEntry(Guid id, StringContent content)
        {
            await client.PutAsync(RequestPasswordStorageData + id, content);
        }
        #endregion PasswordStorageDatas
    }
}
