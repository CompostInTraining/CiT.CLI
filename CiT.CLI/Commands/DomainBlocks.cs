using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using CiT.Core.Entities;
using CiT.Core.Mastodon;
using Newtonsoft.Json.Linq;

namespace CiT.CLI.Commands;

public class DomainBlocks
{
    private readonly string[] _actionArgs;
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
                dynamic blockedDomains = ApiClient.GetInstanceBlockedDomains().Result;
                foreach (JObject item in blockedDomains)
                {
                    var domain = item.GetValue("domain")!.ToString();
                    var severity = item.GetValue("severity")?.ToString();
                    var comment = item.GetValue("comment")?.ToString();
                    Console.WriteLine("{0,-30} {1,-7} {2,-10}",
                        domain, severity, comment);
                }
                break;
            case "query":
                List<BlockedDomain> blocked_domains = ApiClient.GetInstanceBlockedDomains().Result;
                blocked_domains = blocked_domains.OrderBy(bd => bd.Domain).ToList();
                Console.WriteLine(blocked_domains.Any(bd => bd.Domain == _actionArgs[1])
                    ? $"Domain \"{_actionArgs[1]}\" found in blocklist."
                    : $"Domain \"{_actionArgs[1]}\" not found in blocklist.");
                break;
            case "add":
                dynamic? actionResult = null;
                string[] actionArgs = _actionArgs.Skip(1).ToArray();
                switch (actionArgs.Length)
                {
                    case 1:
                        actionResult = ApiClient.AddDomainBlock(actionArgs[0]);
                        break;
                    case 2:
                        actionResult = ApiClient.AddDomainBlock(actionArgs[0], severity: actionArgs[1]);
                        break;
                    case 3:
                        actionResult = ApiClient.AddDomainBlock(actionArgs[0], severity: actionArgs[1], comment: actionArgs[2]);
                        break;
                }
                (int statusCode, string response) result = actionResult!.Result;
                Console.WriteLine($"Status code: {result.statusCode}\n{result.response}");
                
                break;
        }
    }
}
