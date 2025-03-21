namespace CiT.Core.Mastodon;

public class EmailDomainBlocksApi : ApiClient
{
    /// <summary>
    ///     The Mastodon email blocks API URL.
    /// </summary>
    private readonly string? _emailDomainBlocksApiUrl;
    /// <summary>
    ///     Constructs an EmailDomainBlocksApi object using the ConfigManager to set the API URL.
    /// </summary>
    /// <param name="configManager">The ConfigManager.</param>
    /// <param name="client">The HttpClient.</param>
    public EmailDomainBlocksApi(IConfigManager configManager, HttpClient client) : base(configManager, client)
    {
        _emailDomainBlocksApiUrl = $"{configManager.Instance.Url}/api/v1/admin/email_domain_blocks";
    }
    /// <summary>
    ///     Add an email domain to the blocklist.
    /// </summary>
    /// <param name="domain">The domain to add.</param>
    /// <returns>A tuple containing the API response status code and text response.</returns>
    public async Task<(int, string)> AddEmailDomainBlock(string domain)
    {
        Dictionary<string, string> payload = new()
        {
            ["domain"] = domain
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await Client.PostAsync(_emailDomainBlocksApiUrl, stringContent);
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
    /// <summary>
    ///     Get all email domains on the email domain blocklist.
    /// </summary>
    /// <returns>A list of BlockedEmailDomain.</returns>
    public async Task<List<BlockedEmailDomain>> GetInstanceBlockedEmailDomains()
    {
        List<BlockedEmailDomain> rtnObj = new();
        var result = await Client.GetAsync($"{_emailDomainBlocksApiUrl}?limit=200");
        result.EnsureSuccessStatusCode();
        bool linkHeadersFound = result.Headers.TryGetValues("Link", out var linkHeaders);
        var linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders?.First() ?? "");
        string responseContent = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<List<BlockedEmailDomain>>(responseContent)!;
        rtnObj.AddRange(obj);
        while (linkHeadersFound && !string.IsNullOrEmpty(linksFromHeaders.NextLink))
        {
            result = await Client.GetAsync(linksFromHeaders.NextLink);
            result.Headers.TryGetValues("Link", out linkHeaders);
            if (linkHeaders != null) linksFromHeaders = LinkHeader.LinksFromHeader(linkHeaders.First());
            else linksFromHeaders.NextLink = null;
            responseContent = await result.Content.ReadAsStringAsync();
            obj = JsonConvert.DeserializeObject<List<BlockedEmailDomain>>(responseContent);
            if (obj != null) rtnObj.AddRange(obj);
        }
        return rtnObj;
    }
    /// <summary>
    ///     Remove an email domain from the blocklist.
    /// </summary>
    /// <param name="blockedEmailDomain">A BlockedEmailDomain object representing the domain to remove.</param>
    /// <returns>A tuple containing the API response status code and text response.</returns>
    public async Task<(int, string)> RemoveEmailDomainBlock(BlockedEmailDomain blockedEmailDomain)
    {
        var response = await Client.DeleteAsync($"{_emailDomainBlocksApiUrl}/{blockedEmailDomain.Id}");
        string responseString = await response.Content.ReadAsStringAsync();
        return ((int)response.StatusCode, responseString);
    }
}
