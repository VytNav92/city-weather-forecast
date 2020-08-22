using Newtonsoft.Json;

namespace OpenWeatherMapClient.Response.Data
{
    public class SystemData
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sunrise")]
        public int SunriseTime { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }
    }
}
