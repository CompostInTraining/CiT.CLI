using System.Configuration;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Exceptions;
using CiT.Core.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
#pragma warning disable CS8618

namespace CiT.Core.Tests.Configuration;

[TestClass()]
public class ConfigManagerTests
{
    private IConfigManager _configManager;
    private Mock<IConfiguration> _config;
    private InstanceConfiguration _instanceConfiguration;
    [TestInitialize]
    public void Init() {
        _config = new Mock<IConfiguration>();
        _instanceConfiguration = new InstanceConfiguration()
        {
            Url = "https://mastodon.example.com",
            AccessToken = RandomString.New()
        };
        _configManager = new ConfigManager(
            _config.Object, _instanceConfiguration);
    }
    [TestMethod]
    public void ConfigManager_HasProperties() {
        // Assert
        Assert.IsTrue(_configManager.HasProperty("Configuration"));
        Assert.IsTrue(_configManager.HasProperty("Instance"));
    }
    [TestMethod]
    public void GetConfigValue_Success() {
        // Arrange
        var expected = "ExpectedConfig";

        _config.SetupGet(x => x[It.IsAny<string>()]).Returns(expected);

        // Act
        var actual = _configManager.GetConfigValue("ValidName");

        // Assert
        Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    [ExpectedException(typeof(ConfigurationErrorsException))]
    public void GetConfigValue_Throws_ConfigurationErrorsException() {
        // Arrange
        _config.SetupGet(x => x[It.IsAny<string>()]).Returns("");

        // Act
        _configManager.GetConfigValue("InvalidName");

        // Assert - Exception
    }
    [TestMethod]
    [ExpectedException(typeof(InvalidConfigurationException))]
    public void InvalidConfigurationInitialization_Throws_InvalidConfigurationException()
    {
        _config = new Mock<IConfiguration>();
        _instanceConfiguration = new InstanceConfiguration();
        _configManager = new ConfigManager(
            _config.Object, _instanceConfiguration);
    }
}
