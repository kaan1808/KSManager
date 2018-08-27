using Newtonsoft.Json;

namespace KSManager.Api.Client.Model
{
    internal class LoginResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}
