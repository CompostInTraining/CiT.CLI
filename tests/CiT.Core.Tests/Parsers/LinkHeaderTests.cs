using CiT.Core.Parsers;

namespace CiT.Core.Tests.Parsers;

[TestClass]
public class LinkHeaderTests
{
    [TestMethod]
    public void LinksFromHeader_Success()
    {
        // Arrange
        var linkHeaderString = """<https://example.com/next>; rel="next", <https://example.com/prev>; rel="prev", <https://example.com/first>; rel="first", <https://example.com/last>; rel="last" """;

        // Act
        var result = LinkHeader.LinksFromHeader(linkHeaderString);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.NextLink);
        Assert.IsTrue(result.NextLink.Contains("next"));
        Assert.IsNotNull(result.PrevLink);
        Assert.IsTrue(result.PrevLink.Contains("prev"));
        Assert.IsNotNull(result.FirstLink);
        Assert.IsTrue(result.FirstLink.Contains("first"));
        Assert.IsNotNull(result.LastLink);
        Assert.IsTrue(result.LastLink.Contains("last"));
    }
    [TestMethod]
    public void LinksFromHeader_Empty_String_Returns_Null_LinkHeader()
    {
        // Arrange
        var linkHeaderString = "";

        // Act
        var result = LinkHeader.LinksFromHeader(linkHeaderString);

        // Assert
        Assert.IsNull(result);
    }
}
