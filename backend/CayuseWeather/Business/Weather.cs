using Data;

namespace BusinessLogic
{
    public class Weather : IWeather

    {
        IDataLayer _dataLayer;
        public Weather(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
        public WeatherInfo GetWeatherInfoFromZipCode(string zipCode)
        {
          
            return _dataLayer.GetWeatherInfoFromZipCode(zipCode);
        }
    }

    public interface IWeather
    {
        WeatherInfo GetWeatherInfoFromZipCode(string zipCode);

    }
}
