using OpenWeatherMapClient;
using OpenWeatherMapClient.Request;
using OpenWeatherMapClient.Response;
using OpenWeatherMapClient.Response.Data;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WeatherForecastWebApp.Models;

namespace WeatherForecastWebApp.Services
{
    public class WeatherService : IWeatherService
    {
        private static readonly int OkStatusCode = (int)HttpStatusCode.OK;

        private readonly IOpenWeatherClient _openWeatherClient;

        public WeatherService(IOpenWeatherClient openWeatherClient)
        {
            _openWeatherClient = openWeatherClient;
        }

        public async Task<CityWeather> GetCityWeatherAsync(string cityName, UnitType unitType)
        {
            var currentWeatherTask = _openWeatherClient.GetCurrentWeatherAsync(cityName, unitType);
            var hourlyWeather = await _openWeatherClient.GetHourlyWeatherAsync(cityName, unitType);
            var currentWeather = await currentWeatherTask;

            if (currentWeather.StatusCode != OkStatusCode || hourlyWeather.StatusCode != OkStatusCode)
                return null;

            return new CityWeather
            {
                CityInfo = CreateCityInfo(hourlyWeather),
                CurrentWeather = CreateWeatherForecast(currentWeather),
                HourlyWeather = hourlyWeather.HourlyWeatherData.Select(CreateWeatherForecast).ToArray()
            };
        }

        private CityInfo CreateCityInfo(HourlyWeatherResponse weather)
        {
            return new CityInfo
            {
                Country = weather.CityData.Country,
                Name = weather.CityData.Name,
            };
        }

        private WeatherForecast CreateWeatherForecast(ForecastData weather)
        {
            var currentWeatherData = weather.WeatherData.FirstOrDefault();

            return new WeatherForecast
            {
                DateTime = ConvertFromUnixEpoch(weather.DateTime),
                Description = currentWeatherData?.Description,
                Icon = currentWeatherData?.Icon,
                Temperature = (int)Math.Round(weather.TemperatureData.Temperature)
            };
        }

        private DateTime ConvertFromUnixEpoch(double additionalSeconds)
        {
            return DateTime.UnixEpoch.AddSeconds(additionalSeconds);
        }
    }
}
