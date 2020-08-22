using Newtonsoft.Json;

namespace OpenWeatherMapClient.Response.Data
{
    public class ForecastData
    {
        [JsonProperty("clouds")]
        public CloudsData CloudsData { get; set; }

        [JsonProperty("dt")]
        public int DateTime { get; set; }

        [JsonProperty("main")]
        public TemperatureData TemperatureData { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("weather")]
        public WeatherData[] WeatherData { get; set; }

        [JsonProperty("wind")]
        public WindData WindData { get; set; }
    }
}
