using Newtonsoft.Json;

namespace OpenWeatherMapClient.Response.Data
{
    public class WindData
    {
        [JsonProperty("deg")]
        public int Degree { get; set; }

        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}
