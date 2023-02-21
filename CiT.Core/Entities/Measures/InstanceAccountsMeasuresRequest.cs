using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

public class InstanceAccountsMeasuresRequest : MeasuresRequestBase
{
    [JsonProperty("instance_accounts")] public InstanceDomain? InstanceAccounts { get; set; }
    public InstanceAccountsMeasuresRequest() { }
    public InstanceAccountsMeasuresRequest(string domain)
    {
        Keys = "instance_accounts";
        InstanceAccounts = new InstanceDomain
        {
            Domain = domain
        };
    }
}
