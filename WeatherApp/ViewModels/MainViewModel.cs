using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IWeatherService _weatherService;
    private readonly ILocationService _locationService;

    public string TodayDate { get; }

    private string todayDate = string.Empty;

    public MainViewModel(IWeatherService weatherService, ILocationService locationService)
    {
        _weatherService = weatherService;
        _locationService = locationService;
        TodayDate = DateTime.Now.ToString("dddd, d MMMM yyyy");

    }

    [ObservableProperty]
    private string cityName = string.Empty;

    [ObservableProperty]
    private string temperature = "--";

    [ObservableProperty]
    private string description = string.Empty;

    [ObservableProperty]
    private string feelsLike = string.Empty;

    [ObservableProperty]
    private string humidity = string.Empty;

    [ObservableProperty]
    private string windSpeed = string.Empty;

    [ObservableProperty]
    private string iconUrl = string.Empty;

    [ObservableProperty]
    private string statusMessage = "Tap the button to get your weather";

    [ObservableProperty]
    private bool isBusy;

    [RelayCommand]
    private async Task LoadWeatherAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            StatusMessage = "Getting your location...";

            var location = await _locationService.GetCurrentLocationAsync();

            if (location == null)
            {
                StatusMessage = "Couldn't get your location. Check that location is on and allowed for this app.";
                return;
            }

            StatusMessage = "Getting the weather...";

            var weather = await _weatherService.GetWeatherAsync(location.Latitude, location.Longitude);

            if (weather == null)
            {
                StatusMessage = "Couldn't get the weather. Check your internet, or your API key may not be active yet.";
                return;
            }

            var info = weather.Weather.FirstOrDefault();

            CityName = $"{weather.CityName}, {weather.Sys.Country}";
            Temperature = $"{Math.Round(weather.Main.Temp)}°C";
            FeelsLike = $"Feels like {Math.Round(weather.Main.FeelsLike)}°C";
            Humidity = $"{weather.Main.Humidity}%";
            WindSpeed = $"{weather.Wind.Speed} m/s";

            if (info != null)
            {
                Description = char.ToUpper(info.Description[0]) + info.Description.Substring(1);
                IconUrl = $"https://openweathermap.org/img/wn/{info.Icon}@2x.png";
            }

            StatusMessage = "Updated just now";
        }
        finally
        {
            IsBusy = false;
        }
    }
}