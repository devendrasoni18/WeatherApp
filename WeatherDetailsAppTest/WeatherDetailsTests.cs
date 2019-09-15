using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherDetailsApp;

namespace WeatherDetailsApp.Tests
{
    [TestClass]
    public class WeatherDetailsTests
    {

        [TestMethod]
        public void TestWeatherAPIResponeDetails()
        {
            String city = "milan";
            _Default bn = new _Default();
            ResponseWeather rootObject = bn.GetWeatherAPIResponse(city);
            Assert.IsNotNull(rootObject);
            Assert.AreEqual(rootObject.name.ToLower(), city);
        }

        [TestMethod]
        public void TestWeatherDetails()
        {
            String city = "Jaipur";
            _Default bn = new _Default();
            Assert.IsNotNull(bn.GetWeatherDetails(city));
        }

        [TestMethod]
        public void TestWeatherDetailsWithBlankValue()
        {
            String city = "";
            _Default bn = new _Default();
            Assert.AreEqual(bn.GetWeatherDetails(city), "");
        }

        [TestMethod]
        public void TestWeatherDetailsWithIncorrectValue()
        {
            String city = "Capita";
            _Default bn = new _Default();
            Assert.AreEqual(bn.GetWeatherDetails(city),"");
        }
    }
}
