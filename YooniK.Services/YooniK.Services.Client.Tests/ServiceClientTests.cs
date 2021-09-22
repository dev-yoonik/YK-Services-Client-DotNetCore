using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using YooniK.Services.Client.Common;

namespace YooniK.Services.Client.Tests
{
    public class ServiceClientTests
    {
        private Mock<IConnectionInformation> _connectionInformationMock;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _connectionInformationMock = new Mock<IConnectionInformation>();
            _connectionInformationMock.Setup(ci => ci.BaseUrl).Returns("https://localhost:5001");
            _connectionInformationMock.Setup(ci => ci.SubscriptionKey).Returns("x-api-key");
        }

        [Test]
        public void Constructor_NullConnectionInformation_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ServiceClient(null));
        }

        [Test]
        public void Constructor_FilledConnectionInformation_Success()
        {
            Assert.DoesNotThrow(() => new ServiceClient(_connectionInformationMock.Object));
        }
    }
}
