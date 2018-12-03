using System;

namespace Data
{
    public class DataLayer
    {
        public WeatherInfo GetWeatherInfoFromZipCode(string zip)
        {
            var es = new ExternalServices();
            return es.GetWeatherInfoFromZipCode(zip);
        }

    }
}
