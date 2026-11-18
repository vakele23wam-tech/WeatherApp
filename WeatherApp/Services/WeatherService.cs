using System.Globalization;
using System.Net.Http.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherResponse?> GetWeatherAsync(double latitude, double longitude)
    {
        try
        {
            string lat = latitude.ToString(CultureInfo.InvariantCulture);
            string lon = longitude.ToString(CultureInfo.InvariantCulture);

            string url = $"https://api.openweathermap.org/data/2.5/weather" +
                         $"?lat={lat}&lon={lon}" +
                         $"&appid={Secrets.OpenWeatherApiKey}" +
                         $"&units=metric";

            return await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
        }
        catch (Exception)
        {
            return null;
        }
    }
}