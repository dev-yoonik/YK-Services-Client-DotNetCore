using NUnit.Framework;
using Moq;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using YooniK.Services.Client.Common;

namespace YooniK.Services.Client.Tests
{
    public class ServiceClientTests
    {
        [Test]
        public void Constructor_NullConnectionInformation_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ServiceClient(null));
        }

        [Test]
        public void Constructor_FilledConnectionInformation_Success()
        {
            var connectionInformation = new ConnectionInformation("https://localhost:5001", "x-api-key");
            Assert.DoesNotThrow(() => new ServiceClient(connectionInformation));
        }
    }
}
