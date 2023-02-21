using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

public class InstanceFollowsMeasuresRequest : MeasuresRequestBase
{
    [JsonProperty("instance_follows")] public InstanceDomain? InstanceFollows { get; set; }
    public InstanceFollowsMeasuresRequest() { }
    public InstanceFollowsMeasuresRequest(string domain)
    {
        Keys = "instance_follows";
        InstanceFollows = new InstanceDomain
        {
            Domain = domain
        };
    }
}
