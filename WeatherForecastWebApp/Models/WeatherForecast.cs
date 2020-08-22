using System;

namespace WeatherForecastWebApp.Models
{
    public class WeatherForecast
    {
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int Temperature { get; set; }
    }
}
