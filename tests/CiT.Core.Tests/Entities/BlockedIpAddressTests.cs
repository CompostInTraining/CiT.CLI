namespace CiT.Core.Tests.Entities;

[TestClass]
public class BlockedIpAddressTests
{
    [TestMethod]
    public void BlockedIpAddress_HasProperties() {
        // Arrange
        BlockedIpAddress blockedIpAddress = new();

        // Assert
        Assert.AreEqual(7, blockedIpAddress.PropertyCount());
        Assert.IsTrue(blockedIpAddress.HasProperty("Id"));
        Assert.IsTrue(blockedIpAddress.HasProperty("Address"));
        Assert.IsTrue(blockedIpAddress.HasProperty("CreatedAt"));
        Assert.IsTrue(blockedIpAddress.HasProperty("ExpiresAt"));
        Assert.IsTrue(blockedIpAddress.HasProperty("Comment"));
        Assert.IsTrue(blockedIpAddress.HasProperty("Severity"));
        Assert.IsTrue(blockedIpAddress.HasProperty("AddressInfo"));
    }
    [TestMethod]
    public void BlockedIpAddress_NotNull() {
        // Arrange
        var blockedIpAddress = EntityTests.SetProperties(new BlockedIpAddress(), true);

        // Assert
        Assert.IsNotNull(blockedIpAddress);
    }
    [TestMethod]
    public void BlockedIpAddress_NameIsDomain()
    {
        // Arrange
        var blockedIpAddress = new BlockedIpAddress
        {
            Address = "10.200.200.200/32"
        };
        
        // Assert
        Assert.AreEqual(blockedIpAddress.ToString(), blockedIpAddress.Address);
    }
}