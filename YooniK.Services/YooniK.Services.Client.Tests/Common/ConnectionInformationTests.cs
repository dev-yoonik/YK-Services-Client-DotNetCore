using NUnit.Framework;
using System;

namespace YooniK.Services.Client.Common.Tests
{
    public class ConnectionInformationTests
    {
        
        [Test]
        public void Constructor_NullParameters_ThrownArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new ConnectionInformation(null, null));
        }
        
        [Test]
        [TestCase("\t\t", "\n\t")]
        [TestCase("          ", "\t")]
        [TestCase("          ", "           ")]
        public void Constructor_WithEmptyOrWhiteSpaceParameters_ThrowsArgumentException(string baseUrl, string subscriptionKey)
        {
            Assert.Throws<ArgumentException>(() => new ConnectionInformation(baseUrl, subscriptionKey));
        }

        [Test]
        [TestCase("http://localhost:5001/")]
        [TestCase("http://localhost:5004/")]
        [TestCase("http://localhost:5005/")]
        public void Constructor_ValidBaseUrl_Success(string baseUrl)
        {
            // Arrange
            string guid = Guid.NewGuid().ToString();

            // Act
            ConnectionInformation connectionInformation = new ConnectionInformation(baseUrl, guid);

            // Assert
            Assert.AreEqual(baseUrl, connectionInformation.BaseUrl);
        }
        
        [Test]
        [TestCase("f7532fd5-4b80-4922-9393-6709331fc616")]
        [TestCase("5e73f28c-6db8-448b-bc50-107f6291ecae")]
        [TestCase("0bf97c08-b769-4f13-9aa8-549e9761befa")]
        public void Constructor_ValidSubscriptionKey_Success(string subscriptionKey)
        {
            // Arrange

            // Act
            ConnectionInformation connectionInformation = new ConnectionInformation("http://localhost:5005/", subscriptionKey);

            // Assert
            Assert.AreEqual(subscriptionKey, connectionInformation.SubscriptionKey);
        }

    }
}