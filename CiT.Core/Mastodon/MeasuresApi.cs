using System.Collections.Generic;
using System.Threading.Tasks;
using CiT.Core.Configuration;
using CiT.Core.Entities.Measures;
using Newtonsoft.Json;

namespace CiT.Core.Mastodon;

public class MeasuresApi : ApiClient
{
    private static string? _measuresApiUrl;
    public MeasuresApi(IConfigManager configManager) : base(configManager)
    {
        _measuresApiUrl = $"{configManager.Instance.Url}/api/v1/admin/measures";
    }
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
}
