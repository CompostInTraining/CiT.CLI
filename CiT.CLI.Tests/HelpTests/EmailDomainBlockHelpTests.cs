using CiT.CLI.Commands;
using CiT.CLI.Tests.Helpers;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CiT.CLI.Tests;

[TestClass]
public class EmailDomainBlockHelpTests
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
    public void TestEmailDomainBlocksMainHelp()
    {
        // Arrange
        EmailDomainBlocks target = new(new[]
        {
            ""
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.EmailDomainBlocks.Main, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestEmailDomainBlocksShowHelp()
    {
        // Arrange
        EmailDomainBlocks target = new(new[]
        {
            "show", "help"
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.EmailDomainBlocks.Show, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestEmailDomainBlocksQueryHelp()
    {
        // Arrange
        EmailDomainBlocks target = new(new[]
        {
            "query", "help"
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.EmailDomainBlocks.Query, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestEmailDomainBlocksAddHelp()
    {
        // Arrange
        EmailDomainBlocks target = new(new[]
        {
            "add", "help"
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.EmailDomainBlocks.Add, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestEmailDomainBlocksRemoveHelp()
    {
        // Arrange
        EmailDomainBlocks target = new(new[]
        {
            "remove", "help"
        }, _configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        target.Process();

        Assert.AreEqual(Info.EmailDomainBlocks.Remove, consoleOutput.GetOutput());
    }
}
