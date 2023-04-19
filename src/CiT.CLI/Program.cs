using System.Configuration;
using CiT.CLI.Commands;
using Microsoft.Extensions.Configuration;

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
    public static void Main(string[] args)
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
        // Separate subcommands
        string[] actionArgs = args.Skip(1).ToArray();
        if (args.Length == 0)
        {
            Console.WriteLine(Info.Program.Main);
            Environment.Exit(0);
        }
        switch (args[0])
        {
            case "domain-blocks":
                new DomainBlocks(actionArgs, configManager).Process();
                break;
            case "domains":
                new Domains(actionArgs, configManager).Process();
                break;
            case "email-domain-blocks":
                new EmailDomainBlocks(actionArgs, configManager).Process();
                break;
            case "ip-blocks":
                new IpAddressBlocks(actionArgs, configManager).Process();
                break;
            default:
                Console.WriteLine(Info.Program.Main);
                break;
        }
    }
}
