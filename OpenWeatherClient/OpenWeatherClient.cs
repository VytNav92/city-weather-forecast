using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenWeatherMapClient.Request;
using OpenWeatherMapClient.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net;

namespace OpenWeatherMapClient
{
    public class OpenWeatherClient : IOpenWeatherClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenWeatherClient> _logger;
        private readonly OpenWeatherClientOptions _options;

        public OpenWeatherClient(HttpClient httpClient, ILogger<OpenWeatherClient> logger, IOptions<OpenWeatherClientOptions> options)
        {
            if (string.IsNullOrWhiteSpace(options.Value.ApiKey))
                throw new ArgumentException("Parameter is null or contains only whitespaces", nameof(options.Value.ApiKey));

            if (!options.Value.BaseAddressUrl.IsAbsoluteUri)
                throw new ArgumentException("Address is not absolute", nameof(options.Value.BaseAddressUrl));

            if (options.Value.BaseAddressUrl.Scheme != Uri.UriSchemeHttps && options.Value.BaseAddressUrl.Scheme != Uri.UriSchemeHttp)
                throw new ArgumentException("Uri scheme must be http or https", nameof(options.Value.BaseAddressUrl));

            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options.Value;
        }

        public async Task<CurrentWeatherResponse> GetCurrentWeatherAsync(string cityName, UnitType unitType)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                throw new ArgumentException("Parameter is null or contains only whitespaces", nameof(cityName));

            var queryParams = new Dictionary<string, string[]>
            {
                { "q", new [] { cityName } }
            };

            return await GetResponseAsync<CurrentWeatherResponse>("weather", unitType, queryParams).ConfigureAwait(false);
        }

        public async Task<HourlyWeatherResponse> GetHourlyWeatherAsync(string cityName, UnitType unitType)
        {
            if (string.IsNullOrWhiteSpace(cityName))
                throw new ArgumentException("Parameter is null or contains only whitespaces", nameof(cityName));

            var queryParams = new Dictionary<string, string[]>
            {
                { "q", new [] { cityName } }
            };

            return await GetResponseAsync<HourlyWeatherResponse>("forecast", unitType, queryParams).ConfigureAwait(false);
        }

        private string CreateRequestUrl(string action, UnitType unitType, IReadOnlyDictionary<string, string[]> queryParams)
        {
            var allQueryParams = new Dictionary<string, string[]>(queryParams)
            {
                { "appid", new[] { _options.ApiKey } }
            };

            if (unitType != UnitType.Standard)
                allQueryParams.Add("units", new[] { GetUnitQueryParamValue(unitType) });

            var queryString = string.Join("&", allQueryParams.Select(x => $"{x.Key}={string.Join(',', x.Value)}"));
            return $"{_options.BaseAddressUrl}data/2.5/{action}?{queryString}";
        }

        private string GetUnitQueryParamValue(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Imperial:
                    return "imperial";
                case UnitType.Metric:
                    return "metric";
                default:
                    throw new ArgumentException($"Unrecognized unitType: {unitType}");
            }
        }

        private async Task<T> GetResponseAsync<T>(string action, UnitType unitType, IReadOnlyDictionary<string, string[]> queryParameters) where T : IWeatherResponse, new()
        {
            var requestUrl = CreateRequestUrl(action, unitType, queryParameters);

            try
            {
                var response = await _httpClient.GetStringAsync(requestUrl).ConfigureAwait(false);
                _logger.LogDebug($"GET request: {requestUrl} respose: {Environment.NewLine}{response}");

                return JsonConvert.DeserializeObject<T>(response); ;
            }
            catch(HttpRequestException httpException)
            {
                /*
                 * StatusCode property in HttpRequestException will be implemented in .NET 5
                 * Right now there is no better way indicate http status code than checking error message
                 */
                if (httpException.Message.Contains("404 (Not Found)"))
                {
                   return new T
                    {
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }

                throw;
            }
        }
    }
}
