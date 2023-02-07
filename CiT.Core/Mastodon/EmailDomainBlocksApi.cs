using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CiT.Core.Configuration;
using CiT.Core.Entities;
using CiT.Core.Parsers;
using Newtonsoft.Json;

namespace CiT.Core.Mastodon;

public class EmailDomainBlocksApi : ApiClient
{
    private static string? _emailDomainBlocksApiUrl;
    public EmailDomainBlocksApi(IConfigManager configManager) : base(configManager)
    {
        _emailDomainBlocksApiUrl = $"{configManager.Instance.Url}/api/v1/admin/email_domain_blocks";
    }
    public async Task<List<BlockedEmailDomain>> GetInstanceBlockedEmailDomains()
    {
        List<BlockedEmailDomain> rtnObj = new();
        HttpResponseMessage result = await Client.GetAsync($"{_emailDomainBlocksApiUrl}?limit=200");
        result.EnsureSuccessStatusCode();
        result.Headers.TryGetValues("Link", out var linkHeaders);
        var linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders!.First());
        string responseContent = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<BlockedEmailDomain>>(responseContent)!;
        rtnObj.AddRange(obj);
        while (!string.IsNullOrEmpty(linksFromHeaders.NextLink))
        {
            result = await Client.GetAsync(linksFromHeaders.NextLink);
            result.Headers.TryGetValues("Link", out linkHeaders);
            if (linkHeaders != null) linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders.First());
            responseContent = await result.Content.ReadAsStringAsync();
            obj = JsonConvert.DeserializeObject<List<BlockedEmailDomain>>(responseContent);
            if (obj != null) rtnObj.AddRange(obj);
        }
        return rtnObj;
    }
    public async Task<dynamic> AddEmailDomainBlock(string domain)
    {
        Dictionary<string, string> payload = new()
        {
            ["domain"] = domain
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await Client.PostAsync(_emailDomainBlocksApiUrl, stringContent);
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
    public async Task<dynamic> RemoveEmailDomainBlock(BlockedEmailDomain blockedEmailDomain)
    {
        HttpResponseMessage response = await Client.DeleteAsync($"{_emailDomainBlocksApiUrl}/{blockedEmailDomain.Id}");
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
}
