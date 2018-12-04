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
        public WeatherInfo GetWeatherInfoFromZipCode(string zipCode)
        {

            var googleKey = ConfigurationManager.AppSettings["googleKey"];
            var openWeatherKey = ConfigurationManager.AppSettings["openWeatherKey"];
            long unixTimestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

            var weatherInfo = GetWeatherAndLocationFromZip("us", zipCode, openWeatherKey);
            weatherInfo.Timezone = GetTimeZoneFromLocation(weatherInfo.Latitude, weatherInfo.Longitude, googleKey, unixTimestamp.ToString());
            weatherInfo.ElevationMeters = GetElevationFromLocation(weatherInfo.Latitude, weatherInfo.Longitude, googleKey);

            weatherInfo.ElevationFeet = Convert.ToInt32(weatherInfo.ElevationMeters * 3.28);

            return weatherInfo;
        }


        private string GetTimeZoneFromLocation(string latitude, string longitude, string apiKey, string timeStamp)
        {
            var url = $@"https://maps.googleapis.com/maps/api/timezone/json?location={latitude},{longitude}&timestamp={timeStamp}&key={apiKey}";
            var client = new WebClient();
            var response = client.DownloadString(url);
            JObject responseJo = JObject.Parse(response);

            return (string)responseJo["timeZoneName"];
        }

        private int GetElevationFromLocation(string latitude, string longitude, string apiKey)
        {
            var url = $@"https://maps.googleapis.com/maps/api/elevation/json?locations={latitude},{longitude}&key={apiKey}";
            var client = new WebClient();
            var response = client.DownloadString(url);
            JObject responseJo = JObject.Parse(response);
            var elevationMeters = (Int32)responseJo["results"][0]["elevation"];
            return elevationMeters;

        }
        private WeatherInfo GetWeatherAndLocationFromZip(string countryCode, string zip, string apiKey)
        {

            var url = $@"http://api.openweathermap.org/data/2.5/weather?zip={zip},{countryCode}&appid={apiKey}";
            var client = new WebClient();
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
        WeatherInfo GetWeatherInfoFromZipCode(string zipCode);

    }
}
