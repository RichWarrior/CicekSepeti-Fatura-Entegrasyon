using Newtonsoft.Json;

namespace EArchiveClient.DTO.Response
{
    public class GetTokenResponse
    {
        [JsonProperty(PropertyName ="token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "chgpwd")]
        public string Chgpwd { get; set; }

        [JsonProperty(PropertyName = "redirectUrl")]
        public string RedirectUrl { get; set; }
    }
}
