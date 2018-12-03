using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Data
{
    public class ExternalServices : IExternalServices
    {

        public WeatherInfo GetWeatherInfoFromZipCode(string zipCode) 
        {
            long unixTimestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            var weatherInfo = GetWeatherAndLocationFromZip("us", zipCode, "ef9770f9c77287c7722b0dea66ed51b9");
            weatherInfo.Timezone = GetTimeZoneFromLocation(weatherInfo.Latitude, weatherInfo.Longitude, "AIzaSyAnmnqknkOF7Ry-UzB8auoimBpwWv-9sEc", unixTimestamp.ToString());
            weatherInfo.ElevationMeters = GetElevationFromLocation(weatherInfo.Latitude, weatherInfo.Longitude, "AIzaSyAnmnqknkOF7Ry-UzB8auoimBpwWv-9sEc");           
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
                TemperatureCelsius = ((int)responseJo["main"]["temp"] - 273).ToString() ?? "NA",
                TemperatureFahrenheit = ((1.8 * ((int)responseJo["main"]["temp"] - 273)) + 32).ToString() ?? "NA",
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
