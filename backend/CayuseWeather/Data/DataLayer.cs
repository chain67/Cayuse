using System;

namespace Data
{
    public class DataLayer : IDataLayer
    {
        IExternalServices _externalServices;

        public DataLayer(IExternalServices externalServices)
        {
            _externalServices = externalServices;
           
        }
        public WeatherInfo GetWeatherInfoFromZipCode(string zipCode)
        {
            var googleKey = "AIzaSyAnmnqknkOF7Ry-UzB8auoimBpwWv-9sEc";
            var openWeatherKey = "ef9770f9c77287c7722b0dea66ed51b9";


            long unixTimestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

            var weatherInfo = _externalServices.GetWeatherAndLocationFromZip("us", zipCode, openWeatherKey);
            weatherInfo.Timezone = _externalServices.GetTimeZoneFromLocation(weatherInfo.Latitude, weatherInfo.Longitude, googleKey, unixTimestamp.ToString());
            weatherInfo.ElevationMeters = _externalServices.GetElevationFromLocation(weatherInfo.Latitude, weatherInfo.Longitude, googleKey);

            weatherInfo.ElevationFeet = Convert.ToInt32(weatherInfo.ElevationMeters * 3.28);

            return weatherInfo;
            
        }

    }

    public interface IDataLayer
    {
        WeatherInfo GetWeatherInfoFromZipCode(string zip);
    }
}
