using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using OpenWeatherMapClient;
using OpenWeatherMapClient.Request;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace OpenWeatherMapClientTests
{
    [TestFixture]
    public class OpenWeatherClientTests
    {
        private const string API_KEY = "foo";
        private const string BASE_ADDRESS_URL = "https://www.foo.bar";
        private const string CITY_NAME = "bar";
        private readonly ILogger<OpenWeatherClient> _logger;
        private readonly IOptions<OpenWeatherClientOptions> _options;

        public OpenWeatherClientTests()
        {
            _logger = NullLogger<OpenWeatherClient>.Instance;
            _options = Options.Create(new OpenWeatherClientOptions
            {
                ApiKey = API_KEY,
                BaseAddressUrl = new Uri(BASE_ADDRESS_URL)
            });
        }

        #region Constructor tests

        [Test]
        public void OpenWeatherClient_WhenHttpClientIsMissing_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new OpenWeatherClient(null, _logger, _options));
        }

        [Test]
        public void OpenWeatherClient_WhenLoggerIsMissing_ThrowsArgumentNullException()
        {
            using var mockedHttpHandler = new MockHttpMessageHandler();
            Assert.Throws<ArgumentNullException>(() => new OpenWeatherClient(mockedHttpHandler.ToHttpClient(), null, _options));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void OpenWeatherClient_WhenPassingBadApiKey_ThrowsArgumentException(string apiKey)
        {
            using var mockedHttpHandler = new MockHttpMessageHandler();
            var options = Options.Create(new OpenWeatherClientOptions
            {
                ApiKey = apiKey,
                BaseAddressUrl = new Uri(BASE_ADDRESS_URL)
            });
            Assert.Throws<ArgumentException>(() => new OpenWeatherClient(mockedHttpHandler.ToHttpClient(), _logger, options));
        }

        [TestCase("ftp://www.foo.bar")]
        [TestCase("foo://www.foo.bar")]
        public void OpenWeatherClient_WhenPassingNotHttpBaseAddressUrl_ThrowsArgumentException(string baseAddressUrl)
        {
            using var mockedHttpHandler = new MockHttpMessageHandler();
            var options = Options.Create(new OpenWeatherClientOptions
            {
                ApiKey = API_KEY,
                BaseAddressUrl = new Uri(baseAddressUrl)
            });
            Assert.Throws<ArgumentException>(() => new OpenWeatherClient(mockedHttpHandler.ToHttpClient(), _logger, options));
        }

        [Test]
        public void OpenWeatherClient_WhenPassingRelativeBaseAddressUrl_ThrowsArgumentException()
        {
            using var mockedHttpHandler = new MockHttpMessageHandler();
            var options = Options.Create(new OpenWeatherClientOptions
            {
                ApiKey = API_KEY,
                BaseAddressUrl = new Uri("/www/foo/bar", UriKind.Relative)
            });
            Assert.Throws<ArgumentException>(() => new OpenWeatherClient(mockedHttpHandler.ToHttpClient(), _logger, options));
        }

        #endregion

        #region GetCurrentWeatherAsync tests

        [TestCase(UnitType.Imperial, "imperial")]
        [TestCase(UnitType.Metric, "metric")]
        public async Task GetCurrentWeatherAsync_WhenPassingNonStandardUnits_AddsUnitsQueryParam(UnitType unitType, string unitQSParamValue)
        {
            using var mockedHttpHandler = new MockHttpMessageHandler();
            mockedHttpHandler
                .Expect(HttpMethod.Get, $"{BASE_ADDRESS_URL}/data/2.5/weather?q={CITY_NAME}&appid={API_KEY}&units={unitQSParamValue}")
                .Respond(new StringContent("{}"));
            var client = new OpenWeatherClient(mockedHttpHandler.ToHttpClient(), _logger, _options);
            await client.GetCurrentWeatherAsync(CITY_NAME, unitType);
        }

        [Test]
        public async Task GetCurrentWeatherAsync_WhenPassingStandardUnits_DoesNotAddUnitsQueryParam()
        {
            using var mockedHttpHandler = new MockHttpMessageHandler();
            mockedHttpHandler
                .Expect(HttpMethod.Get, $"{BASE_ADDRESS_URL}/data/2.5/weather?q={CITY_NAME}&appid={API_KEY}")
                .Respond(new StringContent("{}"));
            var client = new OpenWeatherClient(mockedHttpHandler.ToHttpClient(), _logger, _options);
            await client.GetCurrentWeatherAsync(CITY_NAME, UnitType.Standard);
        }

        #endregion

        #region GetHourlyWeatherAsync tests
        [TestCase(UnitType.Imperial, "imperial")]
        [TestCase(UnitType.Metric, "metric")]
        public async Task GetHourlyWeatherAsync_WhenPassingNonStandardUnits_AddsUnitsQueryParam(UnitType unitType, string unitQSParamValue)
        {
            using var mockedHttpHandler = new MockHttpMessageHandler();
            mockedHttpHandler
                .Expect(HttpMethod.Get, $"{BASE_ADDRESS_URL}/data/2.5/forecast?q={CITY_NAME}&appid={API_KEY}&units={unitQSParamValue}")
                .Respond(new StringContent("{}"));
            var client = new OpenWeatherClient(mockedHttpHandler.ToHttpClient(), _logger, _options);
            await client.GetHourlyWeatherAsync(CITY_NAME, unitType);
        }

        [Test]
        public async Task GetHourlyWeatherAsync_WhenPassingStandardUnits_DoesNotAddUnitsQueryParam()
        {
            using var mockedHttpHandler = new MockHttpMessageHandler();
            mockedHttpHandler
                .Expect(HttpMethod.Get, $"{BASE_ADDRESS_URL}/data/2.5/forecast?q={CITY_NAME}&appid={API_KEY}")
                .Respond(new StringContent("{}"));
            var client = new OpenWeatherClient(mockedHttpHandler.ToHttpClient(), _logger, _options);
            await client.GetHourlyWeatherAsync(CITY_NAME, UnitType.Standard);
        }

        #endregion
    }
}