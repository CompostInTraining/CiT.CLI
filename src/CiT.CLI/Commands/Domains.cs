using System.CommandLine;

namespace CiT.CLI.Commands;

/// <summary>
///     Domains commands.
/// </summary>
public class Domains
{
    /// <summary>
    ///     The current object's API client.
    /// </summary>
    private readonly MeasuresApi _apiClient;
    /// <summary>
    ///     Constructs a Domains object.
    /// </summary>
    /// <param name="configManager">The ConfigManager.</param>
    public Domains(IConfigManager configManager, HttpClient client)
    {
        _apiClient = new MeasuresApi(configManager, client);
    }
    /// <summary>
    ///     Command to query the instance for information on a specific domain.
    /// </summary>
    private void QueryCommand(string domain)
    {
        var data = new Dictionary<string, string>();
        data["Accounts"] = _apiClient.GetInstanceAccounts(domain).Result.Response![0].Total.ToString();
        data["Media Attachments"] =
            _apiClient.GetInstanceMediaAttachments(domain).Result.Response![0].HumanValue!;
        data["Follows"] = _apiClient.GetInstanceFollows(domain).Result.Response![0].Total.ToString();
        data["Followers"] = _apiClient.GetInstanceFollowers(domain).Result.Response![0].Total.ToString();
        data["Statuses"] = _apiClient.GetInstanceStatuses(domain).Result.Response![0].Total.ToString();
        data["Reports"] = _apiClient.GetInstanceReports(domain).Result.Response![0].Total.ToString();

        string longestKey = data.Select(k => k.Key).OrderByDescending(k => k.Length).First();
        string longestValue = data.Select(i => i.Value).OrderByDescending(v => v.Length).First();
        string strFormat = "{0,-" + longestKey.Length + "} {1," + longestValue.Length + "}";

        Console.WriteLine($"Information for {domain}\n");
        foreach ((string k, string v) in data)
        {
            Console.WriteLine(strFormat,
                k, v);
        }
    }
    public Command GetCommand()
    {
        var domainArg = new Argument<string>("domain");
        
        var command = new Command("domains");
        var queryCommand = new Command("query");
        queryCommand.Add(domainArg);
        queryCommand.SetHandler(QueryCommand, domainArg);
        
        command.AddCommand(queryCommand);
        return command;
    }
}
