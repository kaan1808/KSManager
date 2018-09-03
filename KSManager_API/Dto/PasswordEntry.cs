using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KSManager_API.Dto
{
    public class PasswordEntry
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("icon")]
        public int? Icon { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("securityQuestion")]
        public string SecurityQuestion { get; set; }

        [JsonProperty("securityAnswer")]
        public string SecurityAnswer { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("lastChanges")]
        public DateTime LastChanges { get; set; }
    }
}
