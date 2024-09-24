using System.Net;

namespace CiT.Core.Mastodon;

public class InstanceApi : ApiClient
{
    private static string? _instancesApiUrl;
    public InstanceApi(IConfigManager configManager, HttpClient client) : base(configManager, client)
    {
        _instancesApiUrl = $"{configManager.Instance.Url}/api/v1/instance";
    }
    public async Task<List<string>> GetInstancePeers()
    {
        List<string> results = [];
        var response = await Client.GetAsync($"{_instancesApiUrl}/peers");
        if (response.StatusCode is HttpStatusCode.NotFound)
            throw new InstanceDiscoveryDisabledException();
        var responseContent = await response.Content.ReadAsStringAsync();
        results = JsonConvert.DeserializeObject<List<string>>(responseContent) ?? [];
        return results;
    }
}

public class InstanceDiscoveryDisabledException : Exception
{

}