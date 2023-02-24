using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Extensions;
using CiT.Core.Mastodon;

namespace CiT.CLI.Commands;

/// <summary>
///     IpAddressBlocks commands.
/// </summary>
public class IpAddressBlocks
{
    /// <summary>
    ///     The current object's action arguments.
    /// </summary>
    private readonly string[] _actionArgs;
    /// <summary>
    ///     The current object's API client.
    /// </summary>
    private readonly IpAddressBlocksApi _apiClient;
    /// <summary>
    ///     Constructs an IpAddressBlocks object.
    /// </summary>
    /// <param name="actionArgs">The arguments for the current action.</param>
    /// <param name="configManager">The ConfigManager.</param>
    public IpAddressBlocks(string[] actionArgs, IConfigManager configManager)
    {
        _actionArgs = actionArgs;
        _apiClient = new IpAddressBlocksApi(configManager);
    }
    /// <summary>
    ///     Command to add an IP address to the block/ban list.
    /// </summary>
    private void AddCommand()
    {
        string[] actionArgs = _actionArgs.Skip(1).ToArray();
        var actionResult = _apiClient.AddIpAddressBlock(actionArgs[0], actionArgs[1]);
        (int statusCode, string response) result = actionResult.Result;
        Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
    }
    /// <summary>
    ///     Process subcommand arguments.
    /// </summary>
    public void Process()
    {
        switch (_actionArgs[0])
        {
            case "show":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.IpAddressBlocks.Show);
                    return;
                }
                ShowCommand();
                break;
            case "query":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.IpAddressBlocks.Query);
                    return;
                }
                QueryCommand();
                break;
            case "add":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.IpAddressBlocks.Add);
                    return;
                }
                AddCommand();
                break;
            case "remove":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.IpAddressBlocks.Remove);
                    return;
                }
                RemoveCommand();
                break;
            default:
                Console.WriteLine(Info.IpAddressBlocks.Main);
                break;
        }
    }
    /// <summary>
    ///     Command to query the block/ban list for a specific IP address.
    /// </summary>
    private void QueryCommand()
    {
        var blockedIpAddresses = _apiClient.GetInstanceBlockedIpAddresses().Result;
        if (blockedIpAddresses is null)
        {
            Console.WriteLine("No IP addresses found in blocklist.");
            return;
        }
        var blockedIpAddress =
            blockedIpAddresses.Find(ip => ip.Address == _actionArgs[1]);
        if (blockedIpAddress is not null)
        {
            Console.WriteLine($"IP Address \"{blockedIpAddress.Address}\" found in blocklist.");
            Console.WriteLine(blockedIpAddress.AddressInfo);
        }
        else
        {
            Console.WriteLine($"IP Address \"{_actionArgs[1]}\" not found in blocklist.");
        }
    }
    /// <summary>
    ///     Command to remove an IP address from the block/ban list.
    /// </summary>
    private void RemoveCommand()
    {
        var blockedIpAddresses = _apiClient.GetInstanceBlockedIpAddresses().Result;
        if (blockedIpAddresses is null)
        {
            Console.WriteLine("No IP addresses found in blocklist.");
            return;
        }
        var blockedIpAddress =
            blockedIpAddresses.Find(ip => ip.Address == _actionArgs[1]);
        if (blockedIpAddress is not null)
        {
            var actionResult = _apiClient.RemoveIpAddressBlock(blockedIpAddress);
            (int statusCode, string response) result = actionResult.Result;
            Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
        }
        else
        {
            Console.WriteLine($"IP Address \"{_actionArgs[1]}\" not found in blocklist.");
        }
    }
    /// <summary>
    ///     Command to show all IP addresses on the block/ban list.
    /// </summary>
    private void ShowCommand()
    {
        List<BlockedIpAddress>? blockedIpAddresses = new();
        try
        {
            blockedIpAddresses = _apiClient.GetInstanceBlockedIpAddresses().Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        if (blockedIpAddresses!.Count == 0)
        {
            Console.WriteLine("No IP Address blocks found.");
            return;
        }
        string longestAddress = blockedIpAddresses.OrderByDescending(ip => ip.Address!.Length).First().Address!;
        string longestSeverity = blockedIpAddresses.OrderByDescending(ip => ip.Severity!.Length).First().Severity!;
        var longestCreatedAt =
            blockedIpAddresses.OrderByDescending(ip => ip.CreatedAt.ToString(CultureInfo.InvariantCulture).Length)
                .First().CreatedAt.ToString(CultureInfo.InvariantCulture);
        string strFormat = "{0,-" + longestAddress.Length + "} {1,-" + longestSeverity.Length + "} {2,-" +
                           longestCreatedAt.Length + "}";
        Console.WriteLine(strFormat,
            "IP Address", "Severity", "Created At");
        Console.WriteLine(new string('-',
            longestAddress.Length + 1 + longestSeverity.Length + 1 + longestCreatedAt.Length));
        foreach (var blockedIpAddress in blockedIpAddresses)
        {
            Console.WriteLine(strFormat,
                blockedIpAddress.Address, blockedIpAddress.Severity, blockedIpAddress.CreatedAt);
        }
    }
}
