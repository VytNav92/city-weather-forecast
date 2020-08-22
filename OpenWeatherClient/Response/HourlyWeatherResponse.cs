using Newtonsoft.Json;
using OpenWeatherMapClient.Response.Data;

namespace OpenWeatherMapClient.Response
{
    public class HourlyWeatherResponse : IWeatherResponse
    {
        [JsonProperty("city")]
        public CityData CityData { get; set; }

        [JsonProperty("cnt")]
        public int Count { get; set; }

        [JsonProperty("list")]
        public HourlyWeatherData[] HourlyWeatherData { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("cod")]
        public int StatusCode { get; set; }
    }
}
