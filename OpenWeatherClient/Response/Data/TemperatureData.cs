using Newtonsoft.Json;

namespace OpenWeatherMapClient.Response.Data
{
    public class TemperatureData
    {
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("grnd_level")]
        public int GroundLevel { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }

        [JsonProperty("temp_max")]
        public double MaxTemperature { get; set; }

        [JsonProperty("temp_min")]
        public double MinTemperature { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("sea_level")]
        public int SeaLevel { get; set; }

        [JsonProperty("temp")]
        public double Temperature { get; set; }
    }
}
