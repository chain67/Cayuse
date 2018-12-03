using Data;

namespace BusinessLogic
{
    public class Weather : IWeather
    {
        public WeatherInfo GetWeatherInfoFromZipCode(string zipCode)
        {
            var dl = new DataLayer();
            var weatherInfo = dl.GetWeatherInfoFromZipCode(zipCode);
            return weatherInfo;

        }
    }

    public interface IWeather
    {
        WeatherInfo GetWeatherInfoFromZipCode(string zipCode);

    }
}
