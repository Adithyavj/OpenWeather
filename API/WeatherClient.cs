using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace API
{
    public class WeatherClient
    {
        // access external endpoints
        private readonly HttpClient _httpClient;
        private readonly ServiceSettings _settings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options) // DI recieve httpclient and servicesettings
        {
            _httpClient = httpClient;
            _settings = options.Value;
        }

        // Record types - to store response from the API.
        public record Weather(string description);
        public record Main(decimal temp);
        public record Forecast(Weather[] weather, Main main, long dt);


        public async Task<Forecast> GetCurrentWeatherAsync(string city)
        {
            var forecast = await _httpClient
                        .GetFromJsonAsync<Forecast>($"https://{_settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={_settings.ApiKey}&units=metric");

            return forecast;
        }

    }
}