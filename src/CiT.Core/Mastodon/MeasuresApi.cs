using CiT.Core.Configuration;
using CiT.Core.Entities.Measures;
using Newtonsoft.Json;

namespace CiT.Core.Mastodon;

public class MeasuresApi : ApiClient
{
    /// <summary>
    ///     The Mastodon measures API URL.
    /// </summary>
    private static string? _measuresApiUrl;
    /// <summary>
    ///     Constructs a MeasuresApi object using the ConfigManager to set the API URL.
    /// </summary>
    /// <param name="configManager">The ConfigManager.</param>
    public MeasuresApi(IConfigManager configManager) : base(configManager)
    {
        _measuresApiUrl = $"{configManager.Instance.Url}/api/v1/admin/measures";
    }
    /// <summary>
    ///     Gets instance accounts metrics.
    /// </summary>
    /// <param name="domain">The domain to get metrics for.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<MeasuresResponse> GetInstanceAccounts(string domain)
    {
        var requestData = new InstanceAccountsMeasuresRequest(domain);
        var response = await Client.PostAsync(_measuresApiUrl, requestData.ToStringContent());
        string responseContent = await response.Content.ReadAsStringAsync();
        return new MeasuresResponse
        {
            Response = JsonConvert.DeserializeObject<List<MeasuresResponseObject>>(responseContent)
        };
    }
    /// <summary>
    ///     Gets instance followers metrics.
    /// </summary>
    /// <param name="domain">The domain to get metrics for.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<MeasuresResponse> GetInstanceFollowers(string domain)
    {
        var requestData = new InstanceFollowersMeasuresRequest(domain);
        var response = await Client.PostAsync(_measuresApiUrl, requestData.ToStringContent());
        string responseContent = await response.Content.ReadAsStringAsync();
        return new MeasuresResponse
        {
            Response = JsonConvert.DeserializeObject<List<MeasuresResponseObject>>(responseContent)
        };
    }
    /// <summary>
    ///     Gets instance follows metrics.
    /// </summary>
    /// <param name="domain">The domain to get metrics for.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<MeasuresResponse> GetInstanceFollows(string domain)
    {
        var requestData = new InstanceFollowsMeasuresRequest(domain);
        var response = await Client.PostAsync(_measuresApiUrl, requestData.ToStringContent());
        string responseContent = await response.Content.ReadAsStringAsync();
        return new MeasuresResponse
        {
            Response = JsonConvert.DeserializeObject<List<MeasuresResponseObject>>(responseContent)
        };
    }
    /// <summary>
    ///     Gets instance media attachments metrics.
    /// </summary>
    /// <param name="domain">The domain to get metrics for.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<MeasuresResponse> GetInstanceMediaAttachments(string domain)
    {
        var requestData = new InstanceMediaAttachmentsMeasuresRequest(domain);
        var response = await Client.PostAsync(_measuresApiUrl, requestData.ToStringContent());
        string responseContent = await response.Content.ReadAsStringAsync();
        return new MeasuresResponse
        {
            Response = JsonConvert.DeserializeObject<List<MeasuresResponseObject>>(responseContent)
        };
    }
    /// <summary>
    ///     Gets instance reports metrics.
    /// </summary>
    /// <param name="domain">The domain to get metrics for.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<MeasuresResponse> GetInstanceReports(string domain)
    {
        var requestData = new InstanceReportsMeasuresRequest(domain);
        var response = await Client.PostAsync(_measuresApiUrl, requestData.ToStringContent());
        string responseContent = await response.Content.ReadAsStringAsync();
        return new MeasuresResponse
        {
            Response = JsonConvert.DeserializeObject<List<MeasuresResponseObject>>(responseContent)
        };
    }
    /// <summary>
    ///     Gets instance statuses metrics.
    /// </summary>
    /// <param name="domain">The domain to get metrics for.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public async Task<MeasuresResponse> GetInstanceStatuses(string domain)
    {
        var requestData = new InstanceStatusesMeasuresRequest(domain);
        var response = await Client.PostAsync(_measuresApiUrl, requestData.ToStringContent());
        string responseContent = await response.Content.ReadAsStringAsync();
        return new MeasuresResponse
        {
            Response = JsonConvert.DeserializeObject<List<MeasuresResponseObject>>(responseContent)
        };
    }
}
