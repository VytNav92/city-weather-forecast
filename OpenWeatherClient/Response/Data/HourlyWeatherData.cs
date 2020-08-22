using Newtonsoft.Json;

namespace OpenWeatherMapClient.Response.Data
{
    public class HourlyWeatherData : ForecastData
    {
        [JsonProperty("dt_txt")]
        public string DateTimeFormatted { get; set; }
    }
}
