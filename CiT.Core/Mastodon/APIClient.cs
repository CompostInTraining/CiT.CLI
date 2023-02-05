using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Parsers;
using Newtonsoft.Json;

namespace CiT.Core.Mastodon;

public class ApiClient
{
    private static readonly HttpClient Client = new();
    private static string? _domainBlocksApiUrl;
    public ApiClient(IConfigManager configManager)
    {
        _domainBlocksApiUrl = $"{configManager.Instance.Url}/api/v1/admin/domain_blocks";
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", configManager.Instance.AccessToken);
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    public static async Task<List<BlockedDomain>> GetInstanceBlockedDomains()
    {
        List<BlockedDomain> rtnObj = new();
        HttpResponseMessage result = await Client.GetAsync($"{_domainBlocksApiUrl}?limit=200");
        result.Headers.TryGetValues("Link", out var linkHeaders);
        var linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders.First());
        string responseContent = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<BlockedDomain>>(responseContent)!;
        rtnObj.AddRange(obj);
        while (linksFromHeaders.NextLink is not null)
        {
            result = await Client.GetAsync(linksFromHeaders.NextLink);
            result.Headers.TryGetValues("Link", out linkHeaders);
            linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders.First());
            responseContent = await result.Content.ReadAsStringAsync();
            obj = JsonConvert.DeserializeObject<List<BlockedDomain>>(responseContent);
            rtnObj.AddRange(obj);
        }
        return rtnObj;
    }
    public static async Task<dynamic> AddDomainBlock(string domain, string? severity = null, string? comment = null)
    {
        Dictionary<string, string> payload = new();
        payload["domain"] = domain;
        if (severity is not null)
        {
            payload["severity"] = severity;
        }
        if (comment is not null)
        {
            payload["private_comment"] = comment;
        }
        var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await Client.PostAsync(_domainBlocksApiUrl, stringContent);
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
}
