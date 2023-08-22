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
        .AddJsonFile("clisettings.json", false, true)
        .AddJsonFile($"clisettings.{Environment.GetEnvironmentVariable("CIT_CLI_ENV")}.json", true, true)
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

        var instanceConfig = Configuration.GetSection("Instance").Get<InstanceConfiguration>();
        IConfigManager configManager =
            new ConfigManager(Configuration, instanceConfig ?? throw new InvalidOperationException());

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
        var subCommands = new[]
        {
            new DomainBlocks(configManager).GetCommand(),
            new EmailDomainBlocks(configManager).GetCommand(),
            new Domains(configManager).GetCommand(),
            new IpAddressBlocks(configManager).GetCommand()
        };

        foreach (var command in subCommands)
        {
            rootCommand.Add(command);
        }

        await rootCommand.InvokeAsync(args);
    }
}
