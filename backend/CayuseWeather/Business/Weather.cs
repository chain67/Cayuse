using Data;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public class Weather : IWeather

    {
        IDataLayer _dataLayer;
        public Weather(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
        public string GetWeatherInfoFromZipCode(string zipCode)
        {
            //simple backend validation for US 5 digit zipcodes. 
            //Do not hit remote api if the zipcode does not at least match the regex.
            var regex = @"^\d{5}$";
            var match = Regex.Match(zipCode ?? "", regex, RegexOptions.IgnoreCase);
            var response = @"please enter a valid 5 digit zip code after the url in this format: ?zipCode=97213";

            if (match.Success)
            {
                var weatherInfo = _dataLayer.GetWeatherInfoFromZipCode(zipCode);

               //“At the location $CITY_NAME, the temperature is $TEMPERATURE, the timezone is $TIMEZONE, and the elevation is $ELEVATION”
              response = string.Format("At the location {0}, the temperature is {1}, the timezone is {2}, and the elevation is {3} Feet"
                                       , weatherInfo.CityName, weatherInfo.TemperatureFahrenheit, weatherInfo.Timezone, weatherInfo.ElevationFeet);
            }
           
                return response;       
            
        }
    }

    public interface IWeather
    {
        string GetWeatherInfoFromZipCode(string zipCode);

    }
}
