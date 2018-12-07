using System;
using System.Net;
using Newtonsoft.Json.Linq;
using log4net;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Data
{
    
    public class ExternalServices : IExternalServices
    {
        IWebClientFactory _webClientFactory;

        public ExternalServices(IWebClientFactory webClientFactory)
        {
            _webClientFactory = webClientFactory;

        }
       


        public string GetTimeZoneFromLocation(string latitude, string longitude, string apiKey, string timeStamp)
        {
            var url = $@"https://maps.googleapis.com/maps/api/timezone/json?location={latitude},{longitude}&timestamp={timeStamp}&key={apiKey}";
            var client = _webClientFactory.Create();
            var response = client.DownloadString(url);
            JObject responseJo = JObject.Parse(response);

            return (string)responseJo["timeZoneName"];
        }

       public int GetElevationFromLocation(string latitude, string longitude, string apiKey)
        {
            var url = $@"https://maps.googleapis.com/maps/api/elevation/json?locations={latitude},{longitude}&key={apiKey}";
            var client = _webClientFactory.Create();
            var response = client.DownloadString(url);
            JObject responseJo = JObject.Parse(response);
            var elevationMeters = (Int32)responseJo["results"][0]["elevation"];
            return elevationMeters;

        }
        public WeatherInfo GetWeatherAndLocationFromZip(string countryCode, string zip, string apiKey)
        {

            var url = $@"http://api.openweathermap.org/data/2.5/weather?zip={zip},{countryCode}&appid={apiKey}";
            var client = _webClientFactory.Create();
            var response = client.DownloadString(url);

            JObject responseJo = JObject.Parse(response);

            var weatherInfo = new WeatherInfo
            {
                Latitude = (string)responseJo["coord"]["lat"] ?? "NA",
                Longitude = (string)responseJo["coord"]["lon"] ?? "NA",
                TemperatureCelsius = ((int)responseJo["main"]["temp"] - 273).ToString() + "°C" ?? "NA",
                TemperatureFahrenheit = ((1.8 * ((int)responseJo["main"]["temp"] - 273)) + 32).ToString() + "°F" ?? "NA",
                CityName = (string)responseJo["name"] ?? "NA"
            };

            return weatherInfo;

        }
    }

    public interface IExternalServices
    {
       
        string GetTimeZoneFromLocation(string latitude, string longitude, string apiKey, string timeStamp);
        int GetElevationFromLocation(string latitude, string longitude, string apiKey);
        WeatherInfo GetWeatherAndLocationFromZip(string countryCode, string zip, string apiKey);

    }
}
