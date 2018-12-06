using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        IWeather _weather;
        public SampleDataController(IWeather weather)
        {
            _weather = weather;
        }
        // GET api/weather/?zipCode=97213
        [HttpGet]
        public string WeatherForecast(string zipCode) { 
            var response = _weather.GetWeatherInfoFromZipCode(zipCode);

            /*Typically I would pass this dto back and do interpolation and localization on the front end. 
            since the instructions were specific, I will follow those

            var weatherInfo = _weather.GetWeatherInfoFromZipCode(zipCode);//get a poco from biz
            var weatherInfoDto = new WeatherInfoDto
            {
                CityName = weatherInfo.CityName,
                ElevationFeet = weatherInfo.ElevationFeet.ToString() + " Feet",
                ElevationMeters = weatherInfo.ElevationMeters.ToString() + " Meters",
                TemperatureCelsius = weatherInfo.TemperatureCelsius,
                TemperatureFahrenheit = weatherInfo.TemperatureFahrenheit,
                Timezone = weatherInfo.Timezone

            };*/

            return response;
        }

        /* private static string[] Summaries = new[]
         {
             "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
         };

         [HttpGet("[action]")]
         public IEnumerable<WeatherForecast> WeatherForecasts()
         {
             var rng = new Random();
             return Enumerable.Range(1, 5).Select(index => new WeatherForecast
             {
                 DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                 TemperatureC = rng.Next(-20, 55),
                 Summary = Summaries[rng.Next(Summaries.Length)]
             });
         }

         public class WeatherForecast
         {
             public string DateFormatted { get; set; }
             public int TemperatureC { get; set; }
             public string Summary { get; set; }

             public int TemperatureF
             {
                 get
                 {
                     return 32 + (int)(TemperatureC / 0.5556);
                 }
             }
         }*/
    }
}
