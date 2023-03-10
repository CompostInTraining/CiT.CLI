using CiT.CLI.Commands;
using CiT.CLI.Tests.Helpers;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CiT.CLI.Tests.HelpTests;

[TestClass]
public class DomainsHelpTests
{
    [TestInitialize]
    public void Initialize()
    {
        var instanceConfig = new InstanceConfiguration
        {
            AccessToken = "testing-access-token"
        };
        var moqConfig = new Mock<IConfigManager>();
        moqConfig.Setup(c => c.Instance).Returns(instanceConfig);
        _configManager = moqConfig.Object;
    }
    private IConfigManager _configManager = null!;
    [TestMethod]
    public void TestDomainsMainHelp()
    {
        // Arrange
        Domains target = new(new[]
        {
            ""
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.Domains.Main, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestDomainsQueryHelp()
    {
        // Arrange
        Domains target = new(new[]
        {
            "query", "help"
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.Domains.Query, consoleOutput.GetOutput());
    }
}
