using System.Configuration;
using CiT.CLI.Commands;
using Microsoft.Extensions.Configuration;
using System.CommandLine;

namespace CiT.CLI;

internal static class Program
{
    /// <summary>
    ///     The main program configuration object.
    /// </summary>
    private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .AddYamlFile("clisettings.yaml", false, true)
        .AddYamlFile($"clisettings.{Environment.GetEnvironmentVariable("CIT_CLI_ENV")}.yaml", true, true)
        .AddEnvironmentVariables()
        .Build();
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    /// <param name="args">A string array of arguments passed to the program.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public async static Task Main(string[] args)
    {
        #region Init Config

        IConfigManager configManager =
            new ConfigManager(Configuration);

        #endregion

        bool debug;
        try
        {
            debug = Convert.ToBoolean(configManager.GetConfigValue("Debug"));
        }
        catch (ConfigurationErrorsException)
        {
            debug = false;
        }
        if (debug)
        {
            Console.WriteLine($"Total arguments: {args.Length}");
            Console.Write("Arguments: ");
            foreach (string arg in args)
            {
                Console.Write(arg + ", ");
            }
            Console.Write("\n");
        }

        var rootCommand = new RootCommand("Mastodon Administration CLI");
        var commandClient = new HttpClient();
        var subCommands = new[]
        {
            new DomainBlocks(configManager, commandClient).GetCommand(),
            new EmailDomainBlocks(configManager, commandClient).GetCommand(),
            new Domains(configManager, commandClient).GetCommand(),
            new Instance(configManager, commandClient).GetCommand(),
            new IpAddressBlocks(configManager, commandClient).GetCommand()
        };

        foreach (var command in subCommands)
        {
            rootCommand.Add(command);
        }

        await rootCommand.InvokeAsync(args);
    }
}
