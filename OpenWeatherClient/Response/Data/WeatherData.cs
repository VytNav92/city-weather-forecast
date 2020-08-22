using Newtonsoft.Json;

namespace OpenWeatherMapClient.Response.Data
{
    public class WeatherData
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }
    }
}
