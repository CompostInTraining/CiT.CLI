using System;
using System.Configuration;
using System.Linq;
using CiT.CLI.Commands;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace CiT.CLI;

internal static class Program
{
    private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
        .AddJsonFile("clisettings.json", false, true)
        .AddJsonFile($"clisettings.{Environment.GetEnvironmentVariable("CIT_CLI_ENV")}.json", true, true)
        .AddEnvironmentVariables()
        .Build();
    public static void Main(string[] args)
    {
        #region Init Config

        InstanceConfiguration? instanceConfig = Configuration.GetSection("Instance").Get<InstanceConfiguration>();
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
        string[] actionArgs = args.Skip(1).ToArray();
        if (args.Length == 0)
        {
            Console.WriteLine("No arguments provided.");
            // printHelp(); // TODO
            Environment.Exit(1);
        }
        switch (args[0])
        {
            case "domain-blocks":
                new DomainBlocks(actionArgs, configManager).Process();
                break;
            case "email-domain-blocks":
                new EmailDomainBlocks(actionArgs, configManager).Process();
                break;
            case "ip-blocks":
                new IpAddressBlocks(actionArgs, configManager).Process();
                break;
            default:
                Console.WriteLine("Unknown command");
                break;
        }
    }
}
