using System.CommandLine;
using System.Globalization;

namespace CiT.CLI.Commands;

/// <summary>
///     IpAddressBlocks commands.
/// </summary>
public class IpAddressBlocks
{
    /// <summary>
    ///     The current object's API client.
    /// </summary>
    private readonly IpAddressBlocksApi _apiClient;
    /// <summary>
    ///     Constructs an IpAddressBlocks object.
    /// </summary>
    /// <param name="actionArgs">The arguments for the current action.</param>
    /// <param name="configManager">The ConfigManager.</param>
    public IpAddressBlocks(IConfigManager configManager)
    {
        _apiClient = new IpAddressBlocksApi(configManager);
    }
    /// <summary>
    ///     Command to add an IP address to the block/ban list.
    /// </summary>
    private void AddCommand(string address, string severity)
    {
        var actionResult = _apiClient.AddIpAddressBlock(address, severity);
        (int statusCode, string response) result = actionResult.Result;
        Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
    }
    /// <summary>
    ///     Command to query the block/ban list for a specific IP address.
    /// </summary>
    private void QueryCommand(string address)
    {
        var blockedIpAddresses = _apiClient.GetInstanceBlockedIpAddresses().Result;
        if (blockedIpAddresses is null)
        {
            Console.WriteLine("No IP addresses found in blocklist.");
            return;
        }
        var blockedIpAddress =
            blockedIpAddresses.Find(ip => ip.Address == address);
        if (blockedIpAddress is not null)
        {
            Console.WriteLine($"IP Address \"{blockedIpAddress.Address}\" found in blocklist.");
            Console.WriteLine(blockedIpAddress.AddressInfo);
        }
        else
        {
            Console.WriteLine($"IP Address \"{address}\" not found in blocklist.");
        }
    }
    /// <summary>
    ///     Command to remove an IP address from the block/ban list.
    /// </summary>
    private void RemoveCommand(string address)
    {
        var blockedIpAddresses = _apiClient.GetInstanceBlockedIpAddresses().Result;
        if (blockedIpAddresses is null)
        {
            Console.WriteLine("No IP addresses found in blocklist.");
            return;
        }
        var blockedIpAddress =
            blockedIpAddresses.Find(ip => ip.Address == address);
        if (blockedIpAddress is not null)
        {
            var actionResult = _apiClient.RemoveIpAddressBlock(blockedIpAddress);
            (int statusCode, string response) result = actionResult.Result;
            Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
        }
        else
        {
            Console.WriteLine($"IP Address \"{address}\" not found in blocklist.");
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
    public Command GetCommand()
    {
        var addressArg = new Argument<string>("address");
        var severityOpt = new Option<string>(
            new[]
            {
                "-s", "--severity"
            },
            "should be one of `sign_up_requires_approval`, `sign_up_block`, or `no_access` per the Mastodon docs");
        
        var showCommand = new Command("show");
        showCommand.SetHandler(ShowCommand);
        
        var addCommand = new Command("add");
        addCommand.Add(addressArg);
        addCommand.SetHandler(AddCommand, addressArg, severityOpt);
        
        var queryCommand = new Command("query");
        queryCommand.Add(addressArg);
        queryCommand.SetHandler(QueryCommand, addressArg);
        
        var removeCommand = new Command("remove");
        removeCommand.Add(addressArg);
        removeCommand.SetHandler(RemoveCommand, addressArg);
        
        var command = new Command("ip-address-blocks");
        command.Add(showCommand);
        command.Add(addCommand);
        command.Add(queryCommand);
        command.Add(removeCommand);
        return command;
    }
}
