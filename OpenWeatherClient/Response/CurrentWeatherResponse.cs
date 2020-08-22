using Newtonsoft.Json;
using OpenWeatherMapClient.Response.Data;

namespace OpenWeatherMapClient.Response
{
    public class CurrentWeatherResponse : ForecastData, IWeatherResponse
    {
        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("name")]
        public string CityName { get; set; }

        [JsonProperty("coord")]
        public Coordinate Coordinate { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("cod")]
        public int StatusCode { get; set; }

        [JsonProperty("sys")]
        public SystemData SystemData { get; set; }

        [JsonProperty("timezone")]
        public int TimeZone { get; set; }
    }
}
