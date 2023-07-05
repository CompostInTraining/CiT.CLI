namespace CiT.CLI.Tests.HelpTests;

[TestClass]
public class DomainBlockHelpTests
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
    public void TestDomainBlocksAddHelp()
    {
        // Arrange
        DomainBlocks target = new(_configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        // target.Process();

        Assert.AreEqual(Info.DomainBlocks.Add, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestDomainBlocksMainHelp()
    {
        // Arrange
        DomainBlocks target = new(_configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        // target.Process();

        Assert.AreEqual(Info.DomainBlocks.Main, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestDomainBlocksQueryHelp()
    {
        // Arrange
        DomainBlocks target = new(_configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        // target.Process();

        Assert.AreEqual(Info.DomainBlocks.Query, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestDomainBlocksRemoveHelp()
    {
        // Arrange
        DomainBlocks target = new(_configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        // target.Process();

        Assert.AreEqual(Info.DomainBlocks.Remove, consoleOutput.GetOutput());
    }
    [TestMethod]
    public void TestDomainBlocksShowHelp()
    {
        // Arrange
        DomainBlocks target = new(_configManager);

        // Act
        using var consoleOutput = new ConsoleOutput();
        // target.Process();

        Assert.AreEqual(Info.DomainBlocks.Show, consoleOutput.GetOutput());
    }
}
