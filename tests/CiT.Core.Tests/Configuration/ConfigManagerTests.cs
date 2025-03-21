using System.Configuration;
using CiT.Common.Exceptions;
using CiT.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;
#pragma warning disable CS8618

namespace CiT.Core.Tests.Configuration;

[TestClass()]
public class ConfigManagerTests
{
    private IConfigManager _configManager;
    private InstanceConfiguration _instanceConfiguration;
    [TestInitialize]
    public void Init() {
        var config = new Dictionary<string, string?>
        {
            {
                "Instance:Url", "https://mastodon.example.com"
            },
            {
                "Instance:AccessToken", RandomString.New()
            }
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(config)
            .Build();
        _configManager = new ConfigManager(configuration);
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

        // Act
        var actual = _configManager.GetConfigValue("Instance:Url");

        // Assert
        Assert.AreEqual("https://mastodon.example.com", actual);
    }
    [TestMethod]
    [ExpectedException(typeof(ConfigurationErrorsException))]
    public void GetConfigValue_Throws_ConfigurationErrorsException() {

        // Act
        _configManager.GetConfigValue("InvalidName");

        // Assert - Exception
    }
    [TestMethod]
    public void InvalidConfigurationInitialization_Throws_InvalidConfigurationException()
    {
        var config = new Dictionary<string, string?>
        {
            {
                "Instance", null
            }
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(config)
            .Build();
        Assert.ThrowsException<InvalidConfigurationException>(() => new ConfigManager(configuration));
    }
}
