using System.Net.Http;
using System.Net.Http.Headers;
using CiT.Core.Configuration;

namespace CiT.Core.Mastodon;

public abstract class ApiClient
{
    protected static readonly HttpClient Client = new();
    protected ApiClient(IConfigManager configManager)
    {
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", configManager.Instance.AccessToken);
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Client.DefaultRequestHeaders.Add("User-Agent", "CiT-CLI");
    }
}
