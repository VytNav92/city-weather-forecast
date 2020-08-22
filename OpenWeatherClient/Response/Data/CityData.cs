using Newtonsoft.Json;

namespace OpenWeatherMapClient.Response.Data
{
    public class CityData
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("population")]
        public string Population { get; set; }

        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }

        [JsonProperty("timezone")]
        public int Timezone { get; set; }
    }
}
