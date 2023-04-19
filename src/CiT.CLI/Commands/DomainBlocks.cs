using System;
using System.Collections.Generic;
using System.Linq;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Extensions;
using CiT.Core.Mastodon;

namespace CiT.CLI.Commands;

/// <summary>
///     DomainBlocks commands.
/// </summary>
public class DomainBlocks
{
    /// <summary>
    ///     The current object's action arguments.
    /// </summary>
    private readonly string[] _actionArgs;
    /// <summary>
    ///     The current object's API client.
    /// </summary>
    private readonly DomainBlocksApi _apiClient;
    /// <summary>
    ///     Constructs a DomainBlocks object.
    /// </summary>
    /// <param name="actionArgs">The arguments for the current action.</param>
    /// <param name="configManager">The ConfigManager.</param>
    public DomainBlocks(string[] actionArgs, IConfigManager configManager)
    {
        _actionArgs = actionArgs;
        _apiClient = new DomainBlocksApi(configManager);
    }
    /// <summary>
    ///     Command to add a domain to the blocklist.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when provided arguments can't be processed.</exception>
    private void AddCommand()
    {
        string[] actionArgs = _actionArgs.Skip(1).ToArray();
        dynamic actionResult = actionArgs.Length switch
        {
            1 => _apiClient.AddDomainBlock(actionArgs[0]),
            2 => _apiClient.AddDomainBlock(actionArgs[0], actionArgs[1]),
            3 => _apiClient.AddDomainBlock(actionArgs[0], actionArgs[1], actionArgs[2]),
            _ => throw new InvalidOperationException()
        };
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
                    Console.WriteLine(Info.DomainBlocks.Show);
                    return;
                }
                ShowCommand();
                break;
            case "query":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.DomainBlocks.Query);
                    return;
                }
                QueryCommand();
                break;
            case "add":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.DomainBlocks.Add);
                    return;
                }
                AddCommand();
                break;
            case "remove":
                if (_actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.DomainBlocks.Remove);
                    return;
                }
                RemoveCommand();
                break;
            default:
                Console.WriteLine(Info.DomainBlocks.Main);
                break;
        }
    }
    /// <summary>
    ///     Command to query the blocklist for a single domain.
    /// </summary>
    private void QueryCommand()
    {
        var blockedDomains = _apiClient.GetInstanceBlockedDomains().Result;
        var blockedDomain = blockedDomains.Find(bd => bd.Domain == _actionArgs[1]);
        if (blockedDomain is not null)
        {
            Console.WriteLine($"Domain \"{blockedDomain.Domain}\" found in blocklist.");
            Console.WriteLine(blockedDomain.DomainInfo);
        }
        else
        {
            Console.WriteLine($"Domain \"{_actionArgs[1]}\" not found in blocklist.");
        }
    }
    /// <summary>
    ///     Command to remove a domain from the blocklist.
    /// </summary>
    private void RemoveCommand()
    {
        var blockedDomains = _apiClient.GetInstanceBlockedDomains().Result;
        var blockedDomain = blockedDomains.Find(bd => bd.Domain == _actionArgs[1]);
        if (blockedDomain is not null)
        {
            Console.WriteLine($"Domain \"{blockedDomain.Domain}\" found in blocklist.");
            var actionResult = _apiClient.DeleteInstanceBlockedDomain(blockedDomain);
            (int statusCode, string response) result = actionResult.Result;
            Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
        }
        else
        {
            Console.WriteLine($"Domain \"{_actionArgs[1]}\" not found in blocklist.");
        }
    }
    /// <summary>
    ///     Command to show all domains on the blocklist.
    /// </summary>
    private void ShowCommand()
    {
        List<BlockedDomain>? blockedDomains = null;
        try
        {
            blockedDomains = _apiClient.GetInstanceBlockedDomains().Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        if (blockedDomains.Count == 0)
        {
            Console.WriteLine("No domain blocks found.");
            return;
        }
        string? longestDomain = blockedDomains.OrderByDescending(bd => bd.Domain!.Length).First().Domain;
        string strFormat = "{0,-" + longestDomain!.Length + "} {1,-7} {2,-10}";
        Console.WriteLine(strFormat,
            "Domain", "Severity", "Comment");
        Console.WriteLine(new string('-', longestDomain.Length + 7 + 10));
        foreach (var bd in blockedDomains)
        {
            string comment = bd.PublicComment ?? "";
            if (!string.IsNullOrEmpty(bd.PrivateComment))
            {
                comment += $"// {bd.PrivateComment}";
            }
            Console.WriteLine(strFormat,
                bd.Domain, bd.Severity, comment);
        }
    }
}
