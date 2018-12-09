using NUnit.Framework;
using BusinessLogic;
using Moq;
using Data;


namespace Tests
{

    #region Business
    [TestFixture]
    public class UnitTest
    {


        [Test]
        public void WeatherGetWeatherInfoFromZipCode_ReturnCorrectString_WhenValidZipCode()
        {
            var mockData = new Mock<IDataLayer>();
            mockData = new Mock<IDataLayer>(MockBehavior.Strict);
            mockData.Setup(d => d.GetWeatherInfoFromZipCode("97213")).Returns(Helpers.GetGoodWeatherInfo);

            var _sut = new Weather(mockData.Object);

            var result = _sut.GetWeatherInfoFromZipCode("97213");

            Assert.AreEqual(result, Helpers.GetExpectedGoodWeatherResultString());
        }
        [Test]
        public void WeatherGetWeatherInfoFromZipCode_ReturnCorrectString_WhenInValidZipCode()
        {
            var mockData = new Mock<IDataLayer>();
            mockData = new Mock<IDataLayer>(MockBehavior.Strict);
            mockData.Setup(d => d.GetWeatherInfoFromZipCode("foo")).Returns(Helpers.GetGoodWeatherInfo);

            var _sut = new Weather(mockData.Object);

            var result = _sut.GetWeatherInfoFromZipCode("foo");

            Assert.AreEqual(result, Helpers.GetExpectedBadWeatherResultString());
        }



        [Test]
        public void WeatherGetWeatherInfoFromZipCode_ReturnCorrectString_WhenInvalidZipCode()
        {
            var mockData = new Mock<IDataLayer>();
            mockData = new Mock<IDataLayer>(MockBehavior.Strict);
            mockData.Setup(d => d.GetWeatherInfoFromZipCode("foo")).Returns(Helpers.GetGoodWeatherInfo);

            var _sut = new Weather(mockData.Object);
            var result = _sut.GetWeatherInfoFromZipCode("foo");
            Assert.AreEqual(result, Helpers.GetExpectedBadWeatherResultString());
        }

        #endregion
        #region data

        [Test]
        public void DataLayerGetsWeatherInfoFromZipCode_ReturnsWeatherInfo_WhenValidZipCode()
        {

            var mockExternalServices = new Mock<IExternalServices>();
            mockExternalServices.Setup(es => es.GetElevationFromLocation(It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<string>())).Returns(57);

            mockExternalServices.Setup(es => es.GetTimeZoneFromLocation(It.IsAny<string>(), It.IsAny<string>()
                , It.IsAny<string>(), It.IsAny<string>())).Returns("Pacific Standard Time");

            mockExternalServices.Setup(es => es.GetWeatherAndLocationFromZip(It.IsAny<string>(), It.IsAny<string>()
               , It.IsAny<string>())).Returns(new WeatherInfo {

                   CityName = "Portland",                  
                   Latitude = "45.55",
                   Longitude = "-122.56",
                   ElevationMeters = 57,
                   TemperatureCelsius = "21",
                   TemperatureFahrenheit = "70",
                   
               });

            var _sut = new DataLayer(mockExternalServices.Object);
            var result = _sut.GetWeatherInfoFromZipCode("97213");

            Assert.AreEqual(Helpers.GetGoodWeatherInfo().ElevationFeet, result.ElevationFeet);
            Assert.AreEqual(Helpers.GetGoodWeatherInfo().TemperatureFahrenheit, result.TemperatureFahrenheit);


        }
       
        #endregion
    }
}
