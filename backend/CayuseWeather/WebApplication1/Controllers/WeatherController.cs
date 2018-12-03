using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using WebServices.Models;

namespace  WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        // GET api/weather/?zipCode=97213
        [HttpGet]
        public ActionResult<WeatherInfoDto> Get(string zipCode)
        {
            var weather = new Weather();
            var weatherInfo = weather.GetWeatherInfoFromZipCode(zipCode);
            var weatherInfoDto = new WeatherInfoDto
            {
                CityName = weatherInfo.CityName,
                ElevationFeet = weatherInfo.ElevationFeet,
                ElevationMeters = weatherInfo.ElevationMeters,
                TemperatureCelsius = weatherInfo.TemperatureCelsius,
                TemperatureFahrenheit = weatherInfo.TemperatureFahrenheit,
                Timezone = weatherInfo.Timezone

            };
            return weatherInfoDto;
        }

    }
   
}
