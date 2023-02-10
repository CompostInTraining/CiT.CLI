using System.Configuration;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CiT.Core.Tests.Configuration;

[TestClass()]
public class ConfigManagerTests
{
    private IConfigManager configManager;
    private Mock<IConfiguration> config;
    private InstanceConfiguration instanceConfiguration;
    [TestInitialize]
    public void Init() {
        config = new Mock<IConfiguration>();
        instanceConfiguration = new InstanceConfiguration();
        configManager = new ConfigManager(
            config.Object, instanceConfiguration);
    }
    [TestMethod]
    public void ConfigManager_HasProperties() {
        // Assert
        Assert.IsTrue(configManager.HasProperty("Configuration"));
        Assert.IsTrue(configManager.HasProperty("Instance"));
    }
    [TestMethod]
    public void GetConfigValue_Success() {
        // Arrange
        var expected = "ExpectedConfig";

        config.SetupGet(x => x[It.IsAny<string>()]).Returns(expected);

        // Act
        var actual = configManager.GetConfigValue("ValidName");

        // Assert
        Assert.AreEqual(expected, actual);
    }
    [TestMethod]
    [ExpectedException(typeof(ConfigurationErrorsException))]
    public void GetConfigValue_Throws_ConfigurationErrorsException() {
        // Arrange
        config.SetupGet(x => x[It.IsAny<string>()]).Returns("");

        // Act
        configManager.GetConfigValue("InvalidName");

        // Assert - Exception
    }
}
