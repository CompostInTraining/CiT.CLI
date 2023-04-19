namespace CiT.Core.Tests.Entities;

[TestClass]
public class BlockedDomainTests
{
    [TestMethod]
    public void BlockedDomain_HasProperties() {
        // Arrange
        BlockedDomain blockedDomain = new();

        // Assert
        Assert.AreEqual(10, blockedDomain.PropertyCount());
        Assert.IsTrue(blockedDomain.HasProperty("Id"));
        Assert.IsTrue(blockedDomain.HasProperty("Domain"));
        Assert.IsTrue(blockedDomain.HasProperty("CreatedAt"));
        Assert.IsTrue(blockedDomain.HasProperty("Severity"));
        Assert.IsTrue(blockedDomain.HasProperty("RejectMedia"));
        Assert.IsTrue(blockedDomain.HasProperty("RejectReports"));
        Assert.IsTrue(blockedDomain.HasProperty("PrivateComment"));
        Assert.IsTrue(blockedDomain.HasProperty("PublicComment"));
        Assert.IsTrue(blockedDomain.HasProperty("Obfuscate"));
        Assert.IsTrue(blockedDomain.HasProperty("DomainInfo"));
    }
    [TestMethod]
    public void BlockedDomain_NotNull() {
        // Arrange
        var blockedDomain = EntityTests.SetProperties(new BlockedDomain(), true);

        // Assert
        Assert.IsNotNull(blockedDomain);
    }
    [TestMethod]
    public void BlockedDomain_NameIsDomain()
    {
        // Arrange
        var blockedDomain = new BlockedDomain
        {
            Domain = "example.com"
        };
        
        // Assert
        Assert.AreEqual(blockedDomain.ToString(), blockedDomain.Domain);
    }
}