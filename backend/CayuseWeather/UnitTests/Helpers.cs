using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Tests
{
    public static class Helpers
    {
        public static string GetGoogleTimezoneResponse()
        {
            return "{\r\n   \"dstOffset\" : 0,\r\n   \"rawOffset\" : -28800,\r\n   " +
                "\"status\" : \"OK\",\r\n   " +
                "\"timeZoneId\" : \"America/Los_Angeles\",\r\n   \"timeZoneName\" : " +
                "\"Pacific Standard Time\"\r\n}\r\n";
        }

        public static WeatherInfo GetGoodWeatherInfo()
        {
            return new WeatherInfo
            {
                CityName = "Portland",
                ElevationFeet = 187,
                ElevationMeters = 57,
                Latitude = "45.55",
                Longitude = "-122.56",
                TemperatureCelsius = "21",
                TemperatureFahrenheit = "70",
                Timezone = "Pacific Standard Time"

            };

        }

        public static string GetExpectedGoodWeatherResultString()
        {
            return "At the location Portland, the temperature is 70," +
            " the timezone is Pacific Standard Time, and the elevation is 187 Feet";
          
        }
        public static string GetExpectedBadWeatherResultString()
        {
            return "Please enter a valid 5 digit zip code";
        }

        public static string GetGoodTimeZoneWebClientString()
        {
            var latitude = "45.55";
            var longitude = "-122.56";
            var apiKey = "fakeapiKey";
            var timeStamp = "1544111760";

            return $@"https://maps.googleapis.com/maps/api/timezone/json?location={latitude},{longitude}&timestamp={timeStamp}&key={apiKey}";
        }

        public static string GetGoodElevationWebClientString()
        {
            var latitude = "45.55";
            var longitude = "-122.56";
            var apiKey = "fakeapiKey";
            return $@"https://maps.googleapis.com/maps/api/elevation/json?locations={latitude},{longitude}&key={apiKey}";
        }

        public static string GetGoodWeatherInfoWebClientString()
        {

            var zip = "97213";
            var countryCode = "us";
            var apiKey = "fakeapiKey";

            return $@"http://api.openweathermap.org/data/2.5/weather?zip={zip},{countryCode}&appid={apiKey}";

        }



       // public static string GetBadTimeZoneString() { }

        public static string GetGoogleElevationResponse()
        {
            return "{\r\n   \"results\" : [\r\n      " +
                "{\r\n         \"elevation\" : 57.0084114074707,\r\n        " +
                " \"location\" : {\r\n            \"lat\" : 45.55,\r\n           " +
                " \"lng\" : -122.56\r\n         },\r\n         \"resolution\" : " +
                "4.771975994110107\r\n      }\r\n   ],\r\n   \"status\" : \"OK\"\r\n}";
        }

        public static string GetOpenWeatherResponse()
        {
            return "{\"coord\":{\"lon\":-122.56,\"lat\":45.55},\"weather\"" +
                ":[{\"id\":801,\"main\":\"Clouds\",\"description\":\"few clouds\"" +
                ",\"icon\":\"02d\"}],\"base\":\"stations\",\"main\":{\"temp\":271.84" +
                ",\"pressure\":1020,\"humidity\":53,\"temp_min\":266.45,\"temp_max\":276.45}" +
                ",\"visibility\":16093,\"wind\":{\"speed\":7.2,\"deg\":90,\"gust\":10.3},\"clouds\":{\"all\":20}" +
                ",\"dt\":1544111760,\"sys\":{\"type\":1,\"id\":5321,\"message\":0.004,\"country\":\"US\"" +
                ",\"sunrise\":1544110586,\"sunset\":1544142404},\"id\":420029249,\"name\":\"Portland\"" +
                ",\"cod\":200}";
        }

    }
}
