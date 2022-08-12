using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace YourWeatherInfo_Functions.Tests
{
    public class WeatherRequestTests
    {
        [Fact]
        public async Task Return_200_If_Valid_ZipCode()
        {
            // Given
            Mock<IWeatherService> mockWeatherService = new Mock<IWeatherService>();
            mockWeatherService
                .Setup(x => x.GetWeatherRecord(It.IsAny<ILogger>(), "01970"))
                .ReturnsAsync(new WeatherRecord
                {
                    WeatherRecordJson = "testtestest"
                });

            WeatherRequest weatherRequest = new WeatherRequest(mockWeatherService.Object);

            JObject request = new JObject
            {
                ["zipcode"] = "01970"
            };

            var httpRequest = TestHelpers.HttpRequestSetup(new System.Collections.Generic.Dictionary<string, Microsoft.Extensions.Primitives.StringValues>(), request.ToString());

            // When

            OkObjectResult result = (OkObjectResult)await weatherRequest.Run(httpRequest, NullLogger.Instance);
            var expected = result.Value;

            // Then

            Assert.Equal(200, result.StatusCode);
            Assert.Equal("testtestest", expected);
        }
    }
}
