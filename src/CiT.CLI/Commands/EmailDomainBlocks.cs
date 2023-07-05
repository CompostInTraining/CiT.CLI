using System.CommandLine;

namespace CiT.CLI.Commands;

/// <summary>
///     EmailDomainBlocks commands.
/// </summary>
public class EmailDomainBlocks
{
    /// <summary>
    ///     The current object's API client.
    /// </summary>
    private readonly EmailDomainBlocksApi _apiClient;
    /// <summary>
    ///     Constructs an EmailDomainBlocks object.
    /// </summary>
    /// <param name="actionArgs">The arguments for the current action.</param>
    /// <param name="configManager">The ConfigManager.</param>
    public EmailDomainBlocks(IConfigManager configManager)
    {
        _apiClient = new EmailDomainBlocksApi(configManager);
    }
    /// <summary>
    ///     Command to add a domain to the email domain blocklist.
    /// </summary>
    private void AddCommand(string domain)
    {
        var actionResult = _apiClient.AddEmailDomainBlock(domain);
        (int statusCode, string response) result = actionResult.Result;
        Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
    }
    /// <summary>
    ///     Command to query the email domain blocklist for a specific domain.
    /// </summary>
    private void QueryCommand(string domain)
    {
        var blockedEmailDomains = _apiClient.GetInstanceBlockedEmailDomains().Result;
        var blockedEmailDomain =
            blockedEmailDomains.Find(blockedEmailDomain => blockedEmailDomain.Domain == domain);
        if (blockedEmailDomain is not null)
        {
            Console.WriteLine($"Domain \"{blockedEmailDomain.Domain}\" found in blocklist.");
            Console.WriteLine(blockedEmailDomain.DomainInfo);
        }
        else
        {
            Console.WriteLine($"Domain \"{domain}\" not found in blocklist.");
        }
    }
    /// <summary>
    ///     Command to remove a domain from the email domain blocklist.
    /// </summary>
    private void RemoveCommand(string domain)
    {
        var blockedEmailDomains = _apiClient.GetInstanceBlockedEmailDomains().Result;
        var blockedEmailDomain =
            blockedEmailDomains.Find(blockedEmailDomain => blockedEmailDomain.Domain == domain);
        if (blockedEmailDomain is not null)
        {
            var actionResult = _apiClient.RemoveEmailDomainBlock(blockedEmailDomain);
            (int statusCode, string response) result = actionResult.Result;
            Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
        }
        else
        {
            Console.WriteLine($"Domain \"{domain}\" not found in blocklist.");
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
    public Command GetCommand()
    {
        var domainArg = new Argument<string>("domain");
        
        var showCommand = new Command("show");
        showCommand.SetHandler(ShowCommand);

        var addCommand = new Command("add");
        addCommand.Add(domainArg);
        addCommand.SetHandler(AddCommand, domainArg);
        
        var queryCommand = new Command("query");
        queryCommand.Add(domainArg);
        queryCommand.SetHandler(QueryCommand, domainArg);
        
        var removeCommand = new Command("remove");
        removeCommand.Add(domainArg);
        removeCommand.SetHandler(RemoveCommand, domainArg);
        
        var command = new Command("email-domain-blocks");
        command.Add(showCommand);
        command.Add(addCommand);
        command.Add(queryCommand);
        command.Add(removeCommand);
        return command;
    }
}
