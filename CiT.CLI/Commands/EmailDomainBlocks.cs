using System;
using System.Collections.Generic;
using System.Linq;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Extensions;
using CiT.Core.Mastodon;

namespace CiT.CLI.Commands;

/// <summary>
///     EmailDomainBlocks commands.
/// </summary>
public class EmailDomainBlocks
{
    /// <summary>
    ///     The current object's action arguments.
    /// </summary>
    private readonly string[] _actionArgs;
    /// <summary>
    ///     The current object's API client.
    /// </summary>
    private readonly EmailDomainBlocksApi _apiClient;
    /// <summary>
    ///     Constructs an EmailDomainBlocks object.
    /// </summary>
    /// <param name="actionArgs">The arguments for the current action.</param>
    /// <param name="configManager">The ConfigManager.</param>
    public EmailDomainBlocks(string[] actionArgs, IConfigManager configManager)
    {
        _actionArgs = actionArgs;
        _apiClient = new EmailDomainBlocksApi(configManager);
    }
    /// <summary>
    ///     Command to add a domain to the email domain blocklist.
    /// </summary>
    private void AddCommand()
    {
        string[] actionArgs = _actionArgs.Skip(1).ToArray();
        var actionResult = _apiClient.AddEmailDomainBlock(actionArgs[0]);
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
                    Console.WriteLine(Info.EmailDomainBlocks.Show);
                    return;
                }
                ShowCommand();
                break;
            case "query":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.EmailDomainBlocks.Query);
                    return;
                }
                QueryCommand();
                break;
            case "add":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.EmailDomainBlocks.Add);
                    return;
                }
                AddCommand();
                break;
            case "remove":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.EmailDomainBlocks.Remove);
                    return;
                }
                RemoveCommand();
                break;
            default:
                Console.WriteLine(Info.EmailDomainBlocks.Main);
                break;
        }
    }
    /// <summary>
    ///     Command to query the email domain blocklist for a specific domain.
    /// </summary>
    private void QueryCommand()
    {
        var blockedEmailDomains = _apiClient.GetInstanceBlockedEmailDomains().Result;
        var blockedEmailDomain =
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
    /// <summary>
    ///     Command to remove a domain from the email domain blocklist.
    /// </summary>
    private void RemoveCommand()
    {
        var blockedEmailDomains = _apiClient.GetInstanceBlockedEmailDomains().Result;
        var blockedEmailDomain =
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
    /// <summary>
    ///     Command to show all domains on the email domain blocklist.
    /// </summary>
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
        if (blockedEmailDomains.Count == 0)
        {
            Console.WriteLine("No email domain blocks found.");
            return;
        }
        string? longestDomain = blockedEmailDomains.OrderByDescending(bd => bd.Domain!.Length).First().Domain;
        string strFormat = "{0,-" + longestDomain!.Length + "} {1,-11}";
        Console.WriteLine(strFormat,
            "Domain", "Created At");
        Console.WriteLine(new string('-', longestDomain.Length + 11));
        foreach (var blockedEmailDomain in blockedEmailDomains)
        {
            Console.WriteLine(strFormat,
                blockedEmailDomain.Domain, blockedEmailDomain.CreatedAt);
        }
    }
}
