using System;
using System.Collections.Generic;
using System.Linq;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Mastodon;

namespace CiT.CLI.Commands;

public class EmailDomainBlocks
{
    private readonly string[] _actionArgs;
    private readonly EmailDomainBlocksApi _apiClient;
    public EmailDomainBlocks(string[] actionArgs, IConfigManager configManager)
    {
        _actionArgs = actionArgs;
        _apiClient = new EmailDomainBlocksApi(configManager);
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
        var actionResult = _apiClient.AddEmailDomainBlock(actionArgs[0]);
        (int statusCode, string response) result = actionResult.Result;
        Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
    }
    private void ShowCommand()
    {
        List<BlockedEmailDomain>? blockedEmailDomains = null;
        try
        {
            blockedEmailDomains = _apiClient.GetInstanceBlockedEmailDomains().Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        string? longestDomain = blockedEmailDomains.OrderByDescending(bd => bd.Domain!.Length).First().Domain;
        string strFormat = "{0,-" + longestDomain!.Length + "} {1,-11}";
        Console.WriteLine(strFormat,
            "Domain", "Created At");
        Console.WriteLine(new string('-', longestDomain.Length + 11));
        foreach (BlockedEmailDomain blockedEmailDomain in blockedEmailDomains)
        {
            Console.WriteLine(strFormat,
                blockedEmailDomain.Domain, blockedEmailDomain.CreatedAt);
        }
    }
    private void QueryCommand()
    {
        List<BlockedEmailDomain> blockedEmailDomains = _apiClient.GetInstanceBlockedEmailDomains().Result;
        BlockedEmailDomain? blockedEmailDomain =
            blockedEmailDomains.Find(blockedEmailDomain => blockedEmailDomain.Domain == _actionArgs[1]);
        if (blockedEmailDomain is not null)
        {
            Console.WriteLine($"Domain \"{blockedEmailDomain.Domain}\" found in blocklist.");
            Console.WriteLine(blockedEmailDomain.DomainInfo);
        }
        else
        {
            Console.WriteLine($"Domain \"{_actionArgs[1]}\" not found in blocklist.");
        }
    }
    private void RemoveCommand()
    {
        List<BlockedEmailDomain> blockedEmailDomains = _apiClient.GetInstanceBlockedEmailDomains().Result;
        BlockedEmailDomain? blockedEmailDomain =
            blockedEmailDomains.Find(blockedEmailDomain => blockedEmailDomain.Domain == _actionArgs[1]);
        if (blockedEmailDomain is not null)
        {
            var actionResult = _apiClient.RemoveEmailDomainBlock(blockedEmailDomain);
            (int statusCode, string response) result = actionResult.Result;
            Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
        }
        else
        {
            Console.WriteLine($"Domain \"{_actionArgs[1]}\" not found in blocklist.");
        }
    }
}
