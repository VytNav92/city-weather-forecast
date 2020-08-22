using OpenWeatherMapClient.Request;
using OpenWeatherMapClient.Response;
using System.Threading.Tasks;

namespace OpenWeatherMapClient
{
    public interface IOpenWeatherClient
    {
        Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string cityName, UnitType unitType);
        Task<HourlyWeatherResponse> GetHourlyWeatherAsync(string cityName, UnitType unitType);
    }
}
