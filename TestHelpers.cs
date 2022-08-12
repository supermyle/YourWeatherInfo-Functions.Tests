using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;

namespace YourWeatherInfo_Functions.Tests
{
    public class TestHelpers
    {
        public static HttpRequest HttpRequestSetup(Dictionary<String, StringValues> query, string body, IHeaderDictionary headers = null)
        {
            var reqMock = new Mock<HttpRequest>();
            var httpContextMock = new Mock<HttpContext>();
            var connectionInfoMock = new Mock<ConnectionInfo>();

            reqMock.Setup(req => req.Query).Returns(new QueryCollection(query));
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;
            reqMock.Setup(req => req.Body).Returns(stream);

            connectionInfoMock.Setup(x => x.RemoteIpAddress).Returns(new System.Net.IPAddress(3232235777));

            httpContextMock.Setup(x => x.Connection).Returns(connectionInfoMock.Object);

            reqMock.Setup(req => req.HttpContext).Returns(httpContextMock.Object);

            reqMock.Setup(req => req.Headers).Returns(headers);
            return reqMock.Object;
        }
    }
}
