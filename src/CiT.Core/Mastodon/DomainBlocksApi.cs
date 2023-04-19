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

public class DomainBlocksApi : ApiClient
{
    /// <summary>
    ///     The Mastodon domain blocks API URL.
    /// </summary>
    private static string? _domainBlocksApiUrl;
    /// <summary>
    ///     Constructs a DomainBlocksApi object using the ConfigManager to set the API URL.
    /// </summary>
    /// <param name="configManager">The ConfigManager.</param>
    public DomainBlocksApi(IConfigManager configManager) : base(configManager)
    {
        _domainBlocksApiUrl = $"{configManager.Instance.Url}/api/v1/admin/domain_blocks";
    }
    /// <summary>
    ///     Adds a domain to the blocklist.
    /// </summary>
    /// <param name="domain">The domain to add to the blocklist.</param>
    /// <param name="severity">The severity of the block.</param>
    /// <param name="comment">A private comment to add to the block.</param>
    /// <returns>A tuple containing the API response status code and text response.</returns>
    public async Task<(int, string)> AddDomainBlock(string domain, string? severity = null, string? comment = null)
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
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
        var response = await Client.PostAsync(_domainBlocksApiUrl, stringContent);
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
    /// <summary>
    ///     Remove a domain from the blocklist.
    /// </summary>
    /// <param name="blockedDomain">A BlockedDomain object representing the domain to remove.</param>
    /// <returns>A tuple containing the API response status code and text response.</returns>
    public async Task<(int, string)> DeleteInstanceBlockedDomain(BlockedDomain blockedDomain)
    {
        var response = await Client.DeleteAsync($"{_domainBlocksApiUrl}/{blockedDomain.Id}");
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
    /// <summary>
    ///     Get all domains on the instance blocklist.
    /// </summary>
    /// <returns>A list of BlockedDomain.</returns>
    public async Task<List<BlockedDomain>> GetInstanceBlockedDomains()
    {
        List<BlockedDomain> rtnObj = new();
        var result = await Client.GetAsync($"{_domainBlocksApiUrl}?limit=200");
        result.EnsureSuccessStatusCode();
        bool linkHeadersFound = result.Headers.TryGetValues("Link", out var linkHeaders);
        var linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders?.First() ?? "");
        string responseContent = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<BlockedDomain>>(responseContent)!;
        rtnObj.AddRange(obj);
        while (linkHeadersFound && !string.IsNullOrEmpty(linksFromHeaders.NextLink))
        {
            result = await Client.GetAsync(linksFromHeaders.NextLink);
            result.Headers.TryGetValues("Link", out linkHeaders);
            if (linkHeaders != null) linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders.First());
            responseContent = await result.Content.ReadAsStringAsync();
            obj = JsonConvert.DeserializeObject<List<BlockedDomain>>(responseContent);
            if (obj != null) rtnObj.AddRange(obj);
        }
        return rtnObj;
    }
}
