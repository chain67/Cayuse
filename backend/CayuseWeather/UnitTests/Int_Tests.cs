using NUnit.Framework;
using Moq;
using Data;
using BusinessLogic;


namespace Tests
{
    [TestFixture]
    public class Int_Tests
    {
        [Test]
        public void GetWeatherInfoFromZipCode_ReturnCorrectString_WhenValidZipCode()
        {
            var mockExternalServices = new Mock<IExternalServices>();
            mockExternalServices.Setup(es => es.GetElevationFromLocation(It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<string>())).Returns(57);

            mockExternalServices.Setup(es => es.GetTimeZoneFromLocation(It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<string>(), It.IsAny<string>())).Returns("Pacific Standard Time");

            mockExternalServices.Setup(es => es.GetWeatherAndLocationFromZip(It.IsAny<string>(), It.IsAny<string>()
               , It.IsAny<string>())).Returns(new WeatherInfo
               {

                   CityName = "Portland",
                   Latitude = "45.55",
                   Longitude = "-122.56",
                   ElevationMeters = 57,
                   TemperatureCelsius = "21",
                   TemperatureFahrenheit = "70",

               });
            var data = new DataLayer(mockExternalServices.Object);
            var weather = new Weather(data);
           

           var result =  weather.GetWeatherInfoFromZipCode("97213");

            Assert.AreEqual(Helpers.GetExpectedGoodWeatherResultString(),result);


        }
        [Test]
        public void GetWeatherInfoFromZipCode_ReturnCorrectString_WhenInvalidZipCode()
        {
            var mockExternalServices = new Mock<IExternalServices>();
            mockExternalServices.Setup(es => es.GetElevationFromLocation(It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<string>())).Returns(57);

            mockExternalServices.Setup(es => es.GetTimeZoneFromLocation(It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<string>(), It.IsAny<string>())).Returns("Pacific Standard Time");

            mockExternalServices.Setup(es => es.GetWeatherAndLocationFromZip(It.IsAny<string>(), It.IsAny<string>()
               , It.IsAny<string>())).Returns(new WeatherInfo
               {

                   CityName = "Portland",
                   Latitude = "45.55",
                   Longitude = "-122.56",
                   ElevationMeters = 57,
                   TemperatureCelsius = "21",
                   TemperatureFahrenheit = "70",

               });
            var data = new DataLayer(mockExternalServices.Object);
            var weather = new Weather(data);

            var result = weather.GetWeatherInfoFromZipCode("foo");

            Assert.AreEqual(Helpers.GetExpectedBadWeatherResultString(), result);

        }
    }
}
