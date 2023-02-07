using System;
using System.Collections.Generic;
using System.Linq;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Mastodon;

namespace CiT.CLI.Commands;

public class IpAddressBlocks
{
    private readonly string[] _actionArgs;
    private readonly IpAddressBlocksApi _apiClient;
    public IpAddressBlocks(string[] actionArgs, IConfigManager configManager)
    {
        _actionArgs = actionArgs;
        _apiClient = new IpAddressBlocksApi(configManager);
    }
    public void Process()
    {
        switch (_actionArgs[0])
        {
            case "show":
                ShowCommand();
                break;
            case "query":
                QueryCommand();
                break;
            case "add":
                AddCommand();
                break;
            case "remove":
                RemoveCommand();
                break;
            default:
                Console.WriteLine($"Command \"{_actionArgs[0]}\" not recognized");
                break;
        }
    }
    private void AddCommand()
    {
        string[] actionArgs = _actionArgs.Skip(1).ToArray();
        var actionResult = _apiClient.AddIpAddressBlock(actionArgs[0], actionArgs[1]);
        (int statusCode, string response) result = actionResult.Result;
        Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
    }
    private void ShowCommand()
    {
        List<BlockedIpAddress>? blockedIpAddresses = null;
        try
        {
            blockedIpAddresses = _apiClient.GetInstanceBlockedIpAddresses().Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        if (blockedIpAddresses.Count == 0)
        {
            Console.WriteLine("No IP Address blocks found.");
            return;
        }
        string longestAddress = blockedIpAddresses.OrderByDescending(ip => ip.Address!.Length).First().Address!;
        string longestSeverity = blockedIpAddresses.OrderByDescending(ip => ip.Severity!.Length).First().Severity!;
        string longestCreatedAt =
            blockedIpAddresses.OrderByDescending(ip => ip.CreatedAt.ToString().Length).First().CreatedAt.ToString();
        string strFormat = "{0,-" + longestAddress.Length + "} {1,-" + longestSeverity.Length + "} {2,-" +
                           longestCreatedAt.Length + "}";
        Console.WriteLine(strFormat,
            "IP Address", "Severity", "Created At");
        Console.WriteLine(new string('-',
            longestAddress.Length + 1 + longestSeverity.Length + 1 + longestCreatedAt.Length));
        foreach (BlockedIpAddress blockedIpAddress in blockedIpAddresses)
        {
            Console.WriteLine(strFormat,
                blockedIpAddress.Address, blockedIpAddress.Severity, blockedIpAddress.CreatedAt);
        }
    }
    private void QueryCommand()
    {
        List<BlockedIpAddress> blockedIpAddresses = _apiClient.GetInstanceBlockedIpAddresses().Result;
        BlockedIpAddress blockedIpAddress =
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
    private void RemoveCommand()
    {
        List<BlockedIpAddress> blockedIpAddresses = _apiClient.GetInstanceBlockedIpAddresses().Result;
        BlockedIpAddress? blockedIpAddress =
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
}
