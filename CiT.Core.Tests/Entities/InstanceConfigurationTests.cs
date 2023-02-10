using CiT.Core.Entities;
using CiT.Core.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CiT.Core.Tests.Entities;

[TestClass]
public class InstanceConfigurationTests
{
    [TestMethod]
    public void InstanceConfiguration_HasProperties() {
        // Arrange
        InstanceConfiguration instanceConfiguration = new();

        // Assert
        Assert.AreEqual(2, instanceConfiguration.PropertyCount());
        Assert.IsTrue(instanceConfiguration.HasProperty("Url"));
        Assert.IsTrue(instanceConfiguration.HasProperty("AccessToken"));
    }
    [TestMethod]
    public void InstanceConfiguration_NotNull() {
        // Arrange
        var instanceConfiguration = EntityTests.SetProperties(new InstanceConfiguration(), true);

        // Assert
        Assert.IsNotNull(instanceConfiguration);
    }
}
