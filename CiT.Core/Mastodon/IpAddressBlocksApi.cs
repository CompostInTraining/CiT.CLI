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

public class IpAddressBlocksApi : ApiClient
{
    private static string? _ipAddressBlocksApiUrl;
    public IpAddressBlocksApi(IConfigManager configManager) : base(configManager)
    {
        _ipAddressBlocksApiUrl = $"{configManager.Instance.Url}/api/v1/admin/ip_blocks";
    }
    public async Task<List<BlockedIpAddress>?> GetInstanceBlockedIpAddresses()
    {
        List<BlockedIpAddress> rtnObj = new();
        HttpResponseMessage result = await Client.GetAsync($"{_ipAddressBlocksApiUrl}?limit=200");
        result.EnsureSuccessStatusCode();
        result.Headers.TryGetValues("Link", out var linkHeaders);
        var linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders?.First() ?? "");
        string responseContent = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<BlockedIpAddress>>(responseContent)!;
        rtnObj.AddRange(obj);
        while (!string.IsNullOrEmpty(linksFromHeaders.NextLink))
        {
            result = await Client.GetAsync(linksFromHeaders.NextLink);
            result.Headers.TryGetValues("Link", out linkHeaders);
            if (linkHeaders != null) linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders.First());
            responseContent = await result.Content.ReadAsStringAsync();
            obj = JsonConvert.DeserializeObject<List<BlockedIpAddress>>(responseContent);
            if (obj != null) rtnObj.AddRange(obj);
        }
        return rtnObj;
    }
    public async Task<dynamic> AddIpAddressBlock(string ipAddress, string severity)
    {
        Dictionary<string, string> payload = new()
        {
            ["ip"] = ipAddress,
            ["severity"] = severity
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await Client.PostAsync(_ipAddressBlocksApiUrl, stringContent);
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
    public async Task<dynamic> RemoveIpAddressBlock(BlockedIpAddress blockedIpAddress)
    {
        HttpResponseMessage response = await Client.DeleteAsync($"{_ipAddressBlocksApiUrl}/{blockedIpAddress.Id}");
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
}
