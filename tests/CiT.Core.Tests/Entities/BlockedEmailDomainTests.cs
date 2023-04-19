using CiT.Core.Entities;
using CiT.Core.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CiT.Core.Tests.Entities;

[TestClass]
public class BlockedEmailDomainTests
{
    [TestMethod]
    public void BlockedEmailDomain_HasProperties() {
        // Arrange
        BlockedEmailDomain blockedEmailDomain = new();
        BlockedEmailDomainHistory blockedEmailDomainHistory = new();

        // Assert
        Assert.AreEqual(5, blockedEmailDomain.PropertyCount());
        Assert.IsTrue(blockedEmailDomain.HasProperty("Id"));
        Assert.IsTrue(blockedEmailDomain.HasProperty("Domain"));
        Assert.IsTrue(blockedEmailDomain.HasProperty("CreatedAt"));
        Assert.IsTrue(blockedEmailDomain.HasProperty("History"));
        Assert.IsTrue(blockedEmailDomain.HasProperty("DomainInfo"));
        
        Assert.AreEqual(3, blockedEmailDomainHistory.PropertyCount());
        Assert.IsTrue(blockedEmailDomainHistory.HasProperty("Day"));
        Assert.IsTrue(blockedEmailDomainHistory.HasProperty("Accounts"));
        Assert.IsTrue(blockedEmailDomainHistory.HasProperty("Uses"));
    }
    [TestMethod]
    public void BlockedEmailDomain_NotNull() {
        // Arrange
        var blockedEmailDomain = EntityTests.SetProperties(new BlockedEmailDomain(), true);

        // Assert
        Assert.IsNotNull(blockedEmailDomain);
    }
    [TestMethod]
    public void BlockedEmailDomain_NameIsDomain()
    {
        // Arrange
        var blockedEmailDomain = new BlockedEmailDomain
        {
            Domain = "example.com"
        };
        
        // Assert
        Assert.AreEqual(blockedEmailDomain.ToString(), blockedEmailDomain.Domain);
    }
}