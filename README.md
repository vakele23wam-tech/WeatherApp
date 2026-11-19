# WeatherApp

## Before you run it: add your own API key

The API key is not in this repo, so the app won't build until you add one.

1. Create a free account at https://openweathermap.org/ and copy your key from **My API keys**. New keys can take up to 2 hours to activate.
2. In the `WeatherApp` project folder (next to `MauiProgram.cs`), create a file called `Secrets.cs`:

```csharp
namespace WeatherApp;

public static class Secrets
{
    public const string OpenWeatherApiKey = "YOUR_KEY_HERE";
}
```

3. Replace `YOUR_KEY_HERE` with your key, keeping the quotes.
4. Build and run. Allow location access when the app asks. On an Android emulator, set a location first (Extended controls, then Location).

`Secrets.cs` is listed in `.gitignore`, so it won't be committed.