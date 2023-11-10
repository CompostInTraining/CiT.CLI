namespace CiT.Core.Mastodon;

/// <summary>
///     The base HTTP client for interacting with the Mastodon API.
/// </summary>
public abstract class ApiClient
{
    /// <summary>
    ///     The HttpClient object.
    /// </summary>
    protected static HttpClient Client;
    /// <summary>
    ///     ApiClient constructor to initialize the Authorization, Accept, and User-Agent headers.
    /// </summary>
    /// <param name="configManager">The config manager.</param>
    /// <param name="client">The HttpClient.</param>
    protected ApiClient(IConfigManager configManager, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", configManager.Instance.AccessToken);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", "CiT-CLI");
        Client = client;
    }
}
