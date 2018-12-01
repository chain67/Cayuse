using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestYoke
{
    //Create a .NET Web API that takes ZIP-code as a parameter, then outputs city name, current temperature, time zone, and general elevation at the location with a user-friendly message. For example, 
    //“At the location $CITY_NAME, the temperature is $TEMPERATURE, the timezone is $TIMEZONE, and the elevation is $ELEVATION”. Include documentation with any necessary build instructions and be prepared to discuss your approach.
    //open weather: ef9770f9c77287c7722b0dea66ed51b9
    //api call: API call:
    // api.openweathermap.org/data/2.5/weather? zip = { zip code },{country code}
    /*{"coord":{"lon":-122.09,"lat":37.39},
    "sys":{"type":3,"id":168940,"message":0.0297,"country":"US","sunrise":1427723751,"sunset":1427768967},
    "weather":[{"id":800,"main":"Clear","description":"Sky is Clear","icon":"01n"}],
    "base":"stations",
    "main":{"temp":285.68,"humidity":74,"pressure":1016.8,"temp_min":284.82,"temp_max":286.48},
    "wind":{"speed":0.96,"deg":285.001},
    "clouds":{"all":0},
    "dt":1427700245,
    "id":0,
    "name":"Mountain View",
    "cod":200}*/

    //google api: AIzaSyAnmnqknkOF7Ry-UzB8auoimBpwWv-9sEc
    class TestYoke
    {
        static void Main()
        {
            long unixTimestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            var weatherInfo =  GetWeatherAndLocationFromZip("us", "97702", "ef9770f9c77287c7722b0dea66ed51b9");
            weatherInfo.Timezone = GetTimeZoneFromLocation(weatherInfo.Latitude, weatherInfo.Longitude, "AIzaSyAnmnqknkOF7Ry-UzB8auoimBpwWv-9sEc", unixTimestamp.ToString());
            weatherInfo.ElevationFeet = GetElevationFromLocation(weatherInfo.Latitude, weatherInfo.Longitude, "AIzaSyAnmnqknkOF7Ry-UzB8auoimBpwWv-9sEc");
           
            Console.WriteLine("Hello World!");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static string GetTimeZoneFromLocation(string latitude, string longitude, string apiKey, string timeStamp)
        {
            var url = $@"https://maps.googleapis.com/maps/api/timezone/json?location={latitude},{longitude}&timestamp={timeStamp}&key={apiKey}";
            var client = new WebClient();
            var response = client.DownloadString(url);
            JObject responseJo = JObject.Parse(response);

            return (string)responseJo["timeZoneName"];

        }

        static string GetElevationFromLocation(string latitude, string longitude, string apiKey)
        {
            var url = $@"https://maps.googleapis.com/maps/api/elevation/json?locations={latitude},{longitude}&key={apiKey}";
            var client = new WebClient();
            var response = client.DownloadString(url);
            JObject responseJo = JObject.Parse(response);
            var elevationMeters = (Int32)responseJo["results"][0]["elevation"];
            var elevationFeet = elevationMeters * 3.28084;
            return elevationFeet.ToString();

        }
        static WeatherInfo GetWeatherAndLocationFromZip(string countryCode, string zip, string apiKey)
        {

            var url = $@"http://api.openweathermap.org/data/2.5/weather?zip={zip},{countryCode}&appid={apiKey}";
            var client = new WebClient();
            var response = client.DownloadString(url);

            JObject responseJo = JObject.Parse(response);

            var weatherInfo = new WeatherInfo
            {
                Latitude = (string)responseJo["coord"]["lat"] ?? "NA",
                Longitude = (string)responseJo["coord"]["lon"] ?? "NA",
                TemperatureKelvin = (int)responseJo["main"]["temp"],
                TemperatureCelsius = ((int)responseJo["main"]["temp"] - 273).ToString() ?? "NA",
                TemperatureFahrenheit = ((1.8 * ((int)responseJo["main"]["temp"] - 273)) + 32).ToString() ?? "NA",
                CityName = (string)responseJo["name"] ?? "NA"
            };

            return weatherInfo;


        }
    }
    public class WeatherInfo
    {
        public string CityName { get; set; }
        public int TemperatureKelvin { get; set; }
        public string TemperatureFahrenheit { get; set; }
        public string TemperatureCelsius { get; set; }
        public string Timezone { get; set; }
        public string ElevationFeet { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

    }
    
}
