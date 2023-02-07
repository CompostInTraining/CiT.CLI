using System;
using System.Collections.Generic;
using System.Linq;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Mastodon;

namespace CiT.CLI.Commands;

public class DomainBlocks
{
    private readonly string[] _actionArgs;
    // ReSharper disable once NotAccessedField.Local
    private readonly DomainBlocksApi _apiClient;
    public DomainBlocks(string[] actionArgs, IConfigManager configManager)
    {
        _actionArgs = actionArgs;
        _apiClient = new DomainBlocksApi(configManager);
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
        }
    }
    private void AddCommand()
    {
        string[] actionArgs = _actionArgs.Skip(1).ToArray();
        dynamic actionResult = actionArgs.Length switch
        {
            1 => _apiClient.AddDomainBlock(actionArgs[0]),
            2 => _apiClient.AddDomainBlock(actionArgs[0], severity: actionArgs[1]),
            3 => _apiClient.AddDomainBlock(actionArgs[0], severity: actionArgs[1], comment: actionArgs[2]),
            _ => throw new InvalidOperationException()
        };
        (int statusCode, string response) result = actionResult.Result;
        Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
    }
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
        string? longestDomain = blockedDomains.OrderByDescending(bd => bd.Domain!.Length).First().Domain;
        string strFormat = "{0,-" + longestDomain!.Length + "} {1,-7} {2,-10}";
        Console.WriteLine(strFormat,
            "Domain", "Severity", "Comment");
        Console.WriteLine(new string('-', longestDomain.Length + 7 + 10));
        foreach (BlockedDomain bd in blockedDomains)
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
    private void QueryCommand()
    {
        List<BlockedDomain> blockedDomains = _apiClient.GetInstanceBlockedDomains().Result;
        BlockedDomain? blockedDomain = blockedDomains.Find(bd => bd.Domain == _actionArgs[1]);
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
}
