using System.CommandLine;

namespace CiT.CLI.Commands;

/// <summary>
///     DomainBlocks commands.
/// </summary>
public class DomainBlocks
{
    /// <summary>
    ///     The current object's API client.
    /// </summary>
    private readonly DomainBlocksApi _apiClient;
    /// <summary>
    ///     Constructs a DomainBlocks object.
    /// </summary>
    /// <param name="configManager">The ConfigManager.</param>
    public DomainBlocks(IConfigManager configManager)
    {
        _apiClient = new DomainBlocksApi(configManager);
    }
    /// <summary>
    ///     Command to add a domain to the blocklist.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when provided arguments can't be processed.</exception>
    private void AddCommand(string domain, string? severity, string? comment)
    {
        var actionResult = _apiClient.AddDomainBlock(domain, severity, comment);
        (int statusCode, string response) result = actionResult.Result;
        Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
    }
    /// <summary>
    ///     Command to query the blocklist for a single domain.
    /// </summary>
    private void QueryCommand(string domain)
    {
        var blockedDomains = _apiClient.GetInstanceBlockedDomains().Result;
        var blockedDomain = blockedDomains.Find(bd => bd.Domain == domain);
        if (blockedDomain is not null)
        {
            Console.WriteLine($"Domain \"{blockedDomain.Domain}\" found in blocklist.");
            Console.WriteLine(blockedDomain.DomainInfo);
        }
        else
        {
            Console.WriteLine($"Domain \"{domain}\" not found in blocklist.");
        }
    }
    /// <summary>
    ///     Command to remove a domain from the blocklist.
    /// </summary>
    private void RemoveCommand(string domain)
    {
        var blockedDomains = _apiClient.GetInstanceBlockedDomains().Result;
        var blockedDomain = blockedDomains.Find(bd => bd.Domain == domain);
        if (blockedDomain is not null)
        {
            Console.WriteLine($"Domain \"{blockedDomain.Domain}\" found in blocklist.");
            var actionResult = _apiClient.DeleteInstanceBlockedDomain(blockedDomain);
            (int statusCode, string response) result = actionResult.Result;
            Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
        }
        else
        {
            Console.WriteLine($"Domain \"{domain}\" not found in blocklist.");
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
    public Command GetCommand()
    {
        var domainArg = new Argument<string>("domain");
        var severityArg = new Option<string>(
            new[] {
                "-s",
                "--severity"
            },
            description: "should be one of `suspend`, `silence`, or `noop` per the Mastodon docs",
            getDefaultValue: () => "silence");
        var commentArg = new Option<string>(
            new[]
            {
                "-c",
                "--comment"
            });
        
        var command = new Command("domain-blocks");
        var showCommand = new Command("show");
        showCommand.SetHandler(ShowCommand);

        var addCommand = new Command("add");
        addCommand.Add(domainArg);
        addCommand.Add(severityArg);
        addCommand.Add(commentArg);
        addCommand.SetHandler(AddCommand, domainArg, severityArg, commentArg);

        var removeCommand = new Command("remove");
        removeCommand.Add(domainArg);
        removeCommand.SetHandler(RemoveCommand, domainArg);

        var queryCommand = new Command("query");
        queryCommand.Add(domainArg);
        queryCommand.SetHandler(QueryCommand, domainArg);
        
        command.AddCommand(showCommand);
        command.AddCommand(addCommand);
        command.AddCommand(queryCommand);
        command.AddCommand(removeCommand);
        return command;
    }
}
