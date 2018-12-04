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
        public WeatherInfo GetWeatherInfoFromZipCode(string zip)
        {
            
            return _externalServices.GetWeatherInfoFromZipCode(zip);
        }

    }

    public interface IDataLayer
    {
        WeatherInfo GetWeatherInfoFromZipCode(string zip);
    }
}
