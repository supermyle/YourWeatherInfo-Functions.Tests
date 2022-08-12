using System;
using Xunit;

namespace YourWeatherInfo_Functions.Tests
{
    public class WeatherRequestTests
    {
        [Fact]
        public void Test1()
        {
            string zipcode = "01950";
            Assert.Equal("01950", zipcode);
        }
        [Fact]
        public void Test2()
        {
            string zipcode = "01950";
            Assert.Equal("01915", zipcode);
        }
    }
}
