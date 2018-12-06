using System;
using NUnit.Framework;
using BusinessLogic;
using System.Net;
using Moq;
using Data;

namespace Tests
{

    #region Business
    [TestFixture]
    public class UnitTest
    {



        [OneTimeSetUp]
        public void TestSetup()
        {



        }

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
        public void ExternalServicesGetWeatherInfoFromZipCode_ReturnsWeatherInfo_WhenValidZipCode()
        {
            var mockWebClient = new Mock<IWebClient>();
            mockWebClient = new Mock<IWebClient>(MockBehavior.Strict);
            mockWebClient.Setup(w => w.DownloadString(Helpers.GetGoodElevationWebClientString())).Returns(Helpers.GetGoogleElevationResponse);
            mockWebClient.Setup(w => w.DownloadString(Helpers.GetGoodTimeZoneWebClientString())).Returns(Helpers.GetGoogleTimezoneResponse);
            mockWebClient.Setup(w => w.DownloadString(Helpers.GetOpenWeatherResponse())).Returns(Helpers.GetOpenWeatherResponse);

            var mockWebClientFactory = new Mock<IWebClientFactory>();
            mockWebClientFactory.Setup(wcf => wcf.Create()).Returns(mockWebClient.Object);

            var _sut = new ExternalServices(mockWebClientFactory.Object);

            var result = _sut.GetWeatherInfoFromZipCode("97213");
        }
        [Test]
        public void ExternalServicesGetWeatherInfoFromZipCode_ReturnsReturnsWeatherInfo_WhenInvalidZipCode()
        {

        }

        #endregion
    }
}
