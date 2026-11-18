using Microsoft.Maui.Devices.Sensors;

namespace WeatherApp.Services;

public class LocationService : ILocationService
{
    public async Task<Location?> GetCurrentLocationAsync()
    {
        try
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if (status != PermissionStatus.Granted)
            {
                return null;
            }

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            return await Geolocation.Default.GetLocationAsync(request);
        }
        catch (Exception)
        {
            return null;
        }
    }
}