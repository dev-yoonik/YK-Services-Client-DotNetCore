using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace YooniK.Services.Client.Common.Tests
{
    public class RequestMessageTests
    {
        [Test]
        public void Constructor_ValidParameters_Success()
        {
            // Arrange
            HttpMethod httpMethod = HttpMethod.Delete;
            Dictionary<string, string> header = new Dictionary<string, string>() { { "headerKey", "headerValue" } };
            Dictionary<string, string> queryString = new Dictionary<string, string>() { { "queryStringKey", "queryStringValue" } };
            string urlRelativePath = "gallery/";
            Mock<IRequest> requestMock = new Mock<IRequest>();
            requestMock.Setup(request => request.ToJson()).Returns("{\"name\": \"Atta\"}");

            // Act
            RequestMessage requestMessage = new RequestMessage(
                httpMethod: httpMethod,
                header: header,
                queryString: queryString,
                urlRelativePath: urlRelativePath,
                request: requestMock.Object
            );

            // Assert
            Assert.AreEqual(requestMessage.HttpMethod, httpMethod);
            Assert.AreEqual(requestMessage.Header, header);
            Assert.AreEqual(requestMessage.QueryString, queryString);
            Assert.AreEqual(requestMessage.UrlRelativePath, urlRelativePath);
            Assert.AreEqual(requestMessage.Request.ToJson(), requestMock.Object.ToJson());
        }

        [Test]
        public void Constructor_NullRequiredParameters_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RequestMessage(httpMethod: null));
        }
    }
}
