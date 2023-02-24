using System.Net.Http;
using System.Net.Http.Headers;
using CiT.Core.Configuration;

namespace CiT.Core.Mastodon;

/// <summary>
///     The base HTTP client for interacting with the Mastodon API.
/// </summary>
public abstract class ApiClient
{
    /// <summary>
    ///     The HttpClient object.
    /// </summary>
    protected static readonly HttpClient Client = new();
    /// <summary>
    ///     ApiClient constructor to initialize the Authorization, Accept, and User-Agent headers.
    /// </summary>
    /// <param name="configManager"></param>
    protected ApiClient(IConfigManager configManager)
    {
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", configManager.Instance.AccessToken);
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Client.DefaultRequestHeaders.Add("User-Agent", "CiT-CLI");
    }
}
