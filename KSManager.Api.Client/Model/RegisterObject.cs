using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KSManager.Api.Client.Model
{
    public class RegisterObject
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }
    }
}
