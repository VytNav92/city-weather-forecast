using OpenWeatherMapClient.Request;
using System.Threading.Tasks;
using WeatherForecastWebApp.Models;

namespace WeatherForecastWebApp.Services
{
    public interface IWeatherService
    {
        Task<CityWeather> GetCityWeatherAsync(string cityName, UnitType unitType);
    }
}
