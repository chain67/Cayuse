using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;


namespace  Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        IWeather _weather;
        public WeatherController(IWeather weather)
        {
            _weather = weather;
        }
        // GET api/weather/?zipCode=97213
        [HttpGet]
        public ActionResult<string> Get(string zipCode)
        {
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

    }
   
}
