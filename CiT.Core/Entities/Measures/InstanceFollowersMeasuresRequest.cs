using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

public class InstanceFollowersMeasuresRequest : MeasuresRequestBase
{
    [JsonProperty("instance_followers")] public InstanceDomain? InstanceFollowers { get; set; }
    public InstanceFollowersMeasuresRequest() { }
    public InstanceFollowersMeasuresRequest(string domain)
    {
        Keys = "instance_followers";
        InstanceFollowers = new InstanceDomain
        {
            Domain = domain
        };
    }
}
