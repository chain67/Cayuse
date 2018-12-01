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
            weatherInfo.timezone = GetTimeZoneFromLocation(weatherInfo.latitude, weatherInfo.longitude, "AIzaSyAnmnqknkOF7Ry-UzB8auoimBpwWv-9sEc", unixTimestamp.ToString());
            weatherInfo.elevationFeet = GetElevationFromLocation(weatherInfo.latitude, weatherInfo.longitude, "AIzaSyAnmnqknkOF7Ry-UzB8auoimBpwWv-9sEc");
           
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
            /*"{\"coord\":{\"lon\":-122.56,\"lat\":45.55},\"weather\":[{\"id\":500,\"main\":\"Rain\",\"description\":\"light rain\",\"icon\":\"10n\"}],\"base\":\"stations\",\"main\":{\"temp\":279.56,\"pressure\":1009,\"humidity\":82,\"temp_min\":278.75,\"temp_max\":280.35},\"visibility\":11265,\"wind\":{\"speed\":5.1,\"deg\":180},\"rain\":{\"1h\":0.85},\"clouds\":{\"all\":90},\"dt\":1543626480,\"sys\":{\"type\":1,\"id\":6070,\"message\":0.004,\"country\":\"US\",\"sunrise\":1543678219,\"sunset\":1543710492},\"id\":420029249,\"name\":\"Portland\",\"cod\":200}"*/

            var url = $@"http://api.openweathermap.org/data/2.5/weather?zip={zip},{countryCode}&appid={apiKey}";
            var client = new WebClient();
            var response = client.DownloadString(url);

            JObject responseJo = JObject.Parse(response);

            //construct on instantiation

            var weatherInfo = new WeatherInfo();

            weatherInfo.latitude = (string)responseJo["coord"]["lat"];
            weatherInfo.longitude = (string)responseJo["coord"]["lon"];
            weatherInfo.temperatureKelvin = (int)responseJo["main"]["temp"];
            weatherInfo.temperatureFahrenheit = ((1.8 * (weatherInfo.temperatureKelvin - 273))+32).ToString();
            weatherInfo.temperatureCelsius = (weatherInfo.temperatureKelvin - 273).ToString();
            weatherInfo.cityName = (string)responseJo["name"];

            return weatherInfo;


        }
    }
    public class WeatherInfo
    {
        // $CITY_NAME, the temperature is $TEMPERATURE, the timezone is $TIMEZONE, and the elevation is $ELEVATION”
        public string cityName { get; set; }
        public int temperatureKelvin { get; set; }
        public string temperatureFahrenheit { get; set; }
        public string temperatureCelsius { get; set; }
        public string timezone { get; set; }
        public string elevationFeet { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

    }
    
}
