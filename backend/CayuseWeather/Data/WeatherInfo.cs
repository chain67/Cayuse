using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
   
    public class WeatherInfo
    {
        public string CityName { get; set; }
        public string TemperatureFahrenheit { get; set; }
        public string TemperatureCelsius { get; set; }
        public string Timezone { get; set; }
        public int ElevationFeet { get; set; }
        public int ElevationMeters { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
   
}
