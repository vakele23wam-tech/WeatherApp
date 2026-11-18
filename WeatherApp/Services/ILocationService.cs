using Microsoft.Maui.Devices.Sensors;

namespace WeatherApp.Services;

public interface ILocationService
{
    Task<Location?> GetCurrentLocationAsync();
}