using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenWeatherMapClient.Request;
using System;
using System.Threading.Tasks;
using WeatherForecastWebApp.Services;

namespace WeatherForecastWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        [HttpGet]
        [Route("weather")]
        public async Task<IActionResult> GetCityWeather([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("Bad argument value", nameof(city));

            _logger.LogInformation($"Getting weather information for {city}");

            try
            {
                var weather = await _weatherService.GetCityWeatherAsync(city, UnitType.Metric);
                return weather != null
                    ? new JsonResult(weather)
                    : (IActionResult)new NotFoundResult();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, $"An error occurred while getting {city} weather");
                throw;
            }
        }
    }
}
