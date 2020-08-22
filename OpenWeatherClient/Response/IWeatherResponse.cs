namespace OpenWeatherMapClient.Response
{
    public interface IWeatherResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
