using System;
using System.Collections.Generic;
using System.Linq;
using CiT.Core.Configuration;
using CiT.Core.Extensions;
using CiT.Core.Mastodon;

namespace CiT.CLI.Commands;

public class Domains
{
    private readonly string[] _actionArgs;
    private readonly MeasuresApi _apiClient;
    public Domains(string[] actionArgs, IConfigManager configManager)
    {
        _actionArgs = actionArgs;
        _apiClient = new MeasuresApi(configManager);
    }
    public void Process()
    {
        switch (_actionArgs[0])
        {
            case "show":
            case "query":
                if (_actionArgs.Length > 1 && _actionArgs[1].IsHelp())
                {
                    Console.WriteLine(Info.Domains.Query);
                    return;
                }
                QueryCommand();
                break;
            default:
                Console.WriteLine(Info.Domains.Main);
                break;
        }
    }
    private void QueryCommand()
    {
        var data = new Dictionary<string, string>();
        data["Accounts"] = _apiClient.GetInstanceAccounts(_actionArgs[1]).Result.Response![0].Total.ToString();
        data["Media Attachments"] = _apiClient.GetInstanceMediaAttachments(_actionArgs[1]).Result.Response![0].HumanValue!;
        data["Follows"] = _apiClient.GetInstanceFollows(_actionArgs[1]).Result.Response![0].Total.ToString();
        data["Followers"] = _apiClient.GetInstanceFollowers(_actionArgs[1]).Result.Response![0].Total.ToString();
        data["Statuses"] = _apiClient.GetInstanceStatuses(_actionArgs[1]).Result.Response![0].Total.ToString();
        data["Reports"] = _apiClient.GetInstanceReports(_actionArgs[1]).Result.Response![0].Total.ToString();

        string longestKey = data.Select(k => k.Key).OrderByDescending(k => k.Length).First();
        string longestValue = data.Select(i => i.Value).OrderByDescending(v => v.Length).First();
        string strFormat = "{0,-" + longestKey.Length + "} {1," + longestValue.Length + "}";

        Console.WriteLine($"Information for {_actionArgs[1]}\n");
        foreach ((string k, string v) in data)
        {
            Console.WriteLine(strFormat,
                k, v);
        }
    }
}
