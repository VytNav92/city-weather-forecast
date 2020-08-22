namespace WeatherForecastWebApp.Models
{
    public class CityWeather
    {
        public CityInfo CityInfo { get; set; }
        public WeatherForecast CurrentWeather { get; set; }
        public WeatherForecast[] HourlyWeather { get; set; }
    }
}
