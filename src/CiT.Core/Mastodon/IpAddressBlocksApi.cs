namespace CiT.Core.Mastodon;

public class IpAddressBlocksApi : ApiClient
{
    /// <summary>
    ///     The Mastodon IP address blocks API URL.
    /// </summary>
    private static string? _ipAddressBlocksApiUrl;
    /// <summary>
    ///     Constructs an IpAddressBlocksApi object using the ConfigManager to set the API URL.
    /// </summary>
    /// <param name="configManager">The ConfigManager.</param>
    public IpAddressBlocksApi(IConfigManager configManager, HttpClient client) : base(configManager, client)
    {
        _ipAddressBlocksApiUrl = $"{configManager.Instance.Url}/api/v1/admin/ip_blocks";
    }
    /// <summary>
    ///     Add an IP address to the blocklist.
    /// </summary>
    /// <param name="ipAddress">The IP address to add.</param>
    /// <param name="severity">The severity of the block/ban.</param>
    /// <returns>A tuple containing the API response status code and text response.</returns>
    public async Task<(int, string)> AddIpAddressBlock(string ipAddress, string severity)
    {
        Dictionary<string, string> payload = new()
        {
            ["ip"] = ipAddress,
            ["severity"] = severity
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(_ipAddressBlocksApiUrl, stringContent);
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
    /// <summary>
    ///     Returns all IP addresses on the blocklist.
    /// </summary>
    /// <returns>A list of BlockedIpAddress.</returns>
    public async Task<List<BlockedIpAddress>?> GetInstanceBlockedIpAddresses()
    {
        List<BlockedIpAddress> rtnObj = new();
        var result = await Client.GetAsync($"{_ipAddressBlocksApiUrl}?limit=200");
        result.EnsureSuccessStatusCode();
        bool linkHeadersFound = result.Headers.TryGetValues("Link", out var linkHeaders);
        var linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders?.First() ?? "");
        string responseContent = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<BlockedIpAddress>>(responseContent)!;
        rtnObj.AddRange(obj);
        while (linkHeadersFound && !string.IsNullOrEmpty(linksFromHeaders.NextLink))
        {
            result = await Client.GetAsync(linksFromHeaders.NextLink);
            result.Headers.TryGetValues("Link", out linkHeaders);
            if (linkHeaders != null) linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders.First());
            else linksFromHeaders.NextLink = null;
            responseContent = await result.Content.ReadAsStringAsync();
            obj = JsonConvert.DeserializeObject<List<BlockedIpAddress>>(responseContent);
            if (obj != null) rtnObj.AddRange(obj);
        }
        return rtnObj;
    }
    /// <summary>
    ///     Remove an IP address from the blocklist.
    /// </summary>
    /// <param name="blockedIpAddress">A BlockedIpAddress object representing the address to remove.</param>
    /// <returns>A tuple containing the API response status code and text response.</returns>
    public async Task<(int, string)> RemoveIpAddressBlock(BlockedIpAddress blockedIpAddress)
    {
        var response = await Client.DeleteAsync($"{_ipAddressBlocksApiUrl}/{blockedIpAddress.Id}");
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
}
