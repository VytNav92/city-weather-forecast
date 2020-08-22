using Newtonsoft.Json;

namespace OpenWeatherMapClient.Response.Data
{
    public class Coordinate
    {
        [JsonProperty ("lat")]
        public double Latitude { get; set; }

        [JsonProperty ("lon")]
        public double Longitude { get; set; }
    }
}
