using Newtonsoft.Json;

namespace OpenWeatherMapClient.Response.Data
{
    public class CloudsData
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}
