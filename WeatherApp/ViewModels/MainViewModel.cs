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

    [ObservableProperty]
    private Brush backgroundBrush = CreateBrush("#4A90E2", "#1E3C72");

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

                BackgroundBrush = GetBrushFor(info.Id, info.Icon.EndsWith("n"));
            }

            StatusMessage = "Updated just now";
        }
        finally
        {
            IsBusy = false;
        }
    }

        //  checking out cool theme features based on weather conditions and time of day
            private static Brush CreateBrush(string top, string bottom)
    {
        return new LinearGradientBrush(
            new GradientStopCollection
            {
                new GradientStop(Color.FromArgb(top), 0.0f),
                new GradientStop(Color.FromArgb(bottom), 1.0f)
            },
            new Point(0, 0),
            new Point(0, 1));
    }

    private static Brush GetBrushFor(int id, bool isNight)
    {
        if (id >= 200 && id < 300)   // thunderstorm
            return isNight ? CreateBrush("#090A0F", "#1F1C2C") : CreateBrush("#1F1C2C", "#373B44");

        if (id >= 300 && id < 400)   // drizzle
            return isNight ? CreateBrush("#16222A", "#3A6073") : CreateBrush("#606C88", "#3F4C6B");

        if (id >= 500 && id < 600)   // rain
            return isNight ? CreateBrush("#0F172A", "#232B3E") : CreateBrush("#373B44", "#4286F4");

        if (id >= 600 && id < 700)   // snow
            return isNight ? CreateBrush("#1E293B", "#334155") : CreateBrush("#5B7FA6", "#3A5A80");

        if (id >= 700 && id < 800)   // fog, mist, haze
            return isNight ? CreateBrush("#304352", "#1F2A33") : CreateBrush("#757F9A", "#4B5568");

        if (id == 800)               // clear
            return isNight ? CreateBrush("#0B1026", "#2B3A67") : CreateBrush("#4A90E2", "#1E3C72");

        if (id == 801 || id == 802)  // few or scattered clouds
            return isNight ? CreateBrush("#1C2841", "#2F3E5C") : CreateBrush("#5C8FD6", "#3A5A8C");

        return isNight ? CreateBrush("#232526", "#414345") : CreateBrush("#6F7F8F", "#4A5560");  // overcast
    }
}
