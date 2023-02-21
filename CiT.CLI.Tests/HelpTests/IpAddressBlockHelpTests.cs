using CiT.CLI.Commands;
using CiT.CLI.Tests.Helpers;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CiT.CLI.Tests.HelpTests;

[TestClass]
public class IpAddressHelpTests
{
    private IConfigManager _configManager;
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
    [TestMethod]
    public void TestIpAddressBlocksMainHelp()
    {
        // Arrange
        IpAddressBlocks target = new(new[]
        {
            ""
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.IpAddressBlocks.Main, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestIpAddressBlocksShowHelp()
    {
        // Arrange
        IpAddressBlocks target = new(new[]
        {
            "show", "help"
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.IpAddressBlocks.Show, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestIpAddressBlocksQueryHelp()
    {
        // Arrange
        IpAddressBlocks target = new(new[]
        {
            "query", "help"
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.IpAddressBlocks.Query, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestIpAddressBlocksAddHelp()
    {
        // Arrange
        IpAddressBlocks target = new(new[]
        {
            "add", "help"
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.IpAddressBlocks.Add, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestIpAddressBlocksRemoveHelp()
    {
        // Arrange
        IpAddressBlocks target = new(new[]
        {
            "remove", "help"
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.IpAddressBlocks.Remove, consoleOutput.GetOutput());
    }
}
