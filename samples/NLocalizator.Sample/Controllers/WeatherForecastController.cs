using Microsoft.AspNetCore.Mvc;

namespace NLocalizator.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    
    private readonly Localizator<WeatherForecastLocalizationBook> _weatherLocalizator;
    private readonly Localizator<DaysLocalizationBook> _daysLocalizator;
    

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, Localizator<WeatherForecastLocalizationBook> weatherLocalizator, Localizator<DaysLocalizationBook> daysLocalizator)
    {
        _logger = logger;
        _weatherLocalizator = weatherLocalizator;
        _daysLocalizator = daysLocalizator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        var freezing = _weatherLocalizator.GetByName("Freezing");
        var hot = _weatherLocalizator.GetByName("Hot");
        
        
        var today = _daysLocalizator.LocalizationBook.Monday;

        var summaries = new string[] { freezing, hot};  

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Day = today,
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost(Name = "SetLanguage")]
    public bool SetLanguage([FromQuery] string language)
    {
        _daysLocalizator.ChangeLanguage(language);
        _weatherLocalizator.ChangeLanguage(language);

        return true;
    }
}