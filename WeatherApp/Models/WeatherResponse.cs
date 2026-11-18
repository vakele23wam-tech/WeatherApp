using System.Text.Json.Serialization;

namespace WeatherApp.Models;

public class WeatherResponse
{
    [JsonPropertyName("name")]
    public string CityName { get; set; } = string.Empty;

    [JsonPropertyName("weather")]
    public List<WeatherInfo> Weather { get; set; } = new();

    [JsonPropertyName("main")]
    public MainInfo Main { get; set; } = new();

    [JsonPropertyName("wind")]
    public WindInfo Wind { get; set; } = new();

    [JsonPropertyName("sys")]
    public SysInfo Sys { get; set; } = new();
}

public class WeatherInfo
{
    [JsonPropertyName("main")]
    public string Main { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;
}

public class MainInfo
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }

    [JsonPropertyName("feels_like")]
    public double FeelsLike { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
}

public class WindInfo
{
    [JsonPropertyName("speed")]
    public double Speed { get; set; }
}

public class SysInfo
{
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;
}