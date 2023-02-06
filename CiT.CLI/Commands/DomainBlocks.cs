using System;
using System.Collections.Generic;
using System.Linq;
using CiT.Core.Entities;
using CiT.Core.Mastodon;

namespace CiT.CLI.Commands;

public class DomainBlocks
{
    private readonly string[] _actionArgs;
    // ReSharper disable once NotAccessedField.Local
    private readonly ApiClient _apiClient;
    public DomainBlocks(string[] actionArgs, ApiClient apiClient)
    {
        _actionArgs = actionArgs;
        _apiClient = apiClient;
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
            1 => ApiClient.AddDomainBlock(actionArgs[0]),
            2 => ApiClient.AddDomainBlock(actionArgs[0], severity: actionArgs[1]),
            3 => ApiClient.AddDomainBlock(actionArgs[0], severity: actionArgs[1], comment: actionArgs[2]),
            _ => throw new InvalidOperationException()
        };
        (int statusCode, string response) result = actionResult.Result;
        Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
    }
    private static void ShowCommand()
    {
        List<BlockedDomain>? blockedDomains = null;
        try
        {
            blockedDomains = ApiClient.GetInstanceBlockedDomains().Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Environment.Exit(1);
        }
        Console.WriteLine("{0,-30} {1,-7} {2,-10}",
            "Domain", "Severity", "Comment");
        foreach (BlockedDomain bd in blockedDomains)
        {
            string comment = bd.PublicComment ?? "";
            if (!string.IsNullOrEmpty(bd.PrivateComment))
            {
                comment += $"// {bd.PrivateComment}";
            }
            Console.WriteLine("{0,-30} {1,-7} {2,-10}",
                bd.Domain, bd.Severity, comment);
        }
    }
    private void QueryCommand()
    {
        List<BlockedDomain> blockedDomains = ApiClient.GetInstanceBlockedDomains().Result;
        BlockedDomain? blockedDomain = blockedDomains.Find(bd => bd.Domain == _actionArgs[1]);
        if (blockedDomain is not null)
        {
            Console.WriteLine($"Domain \"{blockedDomain.Domain}\" found in blocklist.");
            Console.WriteLine($"Domain: {blockedDomain.Domain}\n" +
                              $"Created At: {blockedDomain.CreatedAt}\n" +
                              $"Public Comment: {blockedDomain.PublicComment}\n" +
                              $"Private Comment: {blockedDomain.PrivateComment}\n" +
                              $"Severity: {blockedDomain.Severity}");
        }
        else
        {
            Console.WriteLine($"Domain \"{_actionArgs[1]}\" not found in blocklist.");
        }
    }
}
