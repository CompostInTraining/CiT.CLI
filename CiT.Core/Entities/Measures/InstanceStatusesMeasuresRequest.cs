using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

public class InstanceStatusesMeasuresRequest : MeasuresRequestBase
{
    [JsonProperty("instance_statuses")] public InstanceDomain? InstanceStatuses { get; set; }
    public InstanceStatusesMeasuresRequest() { }
    public InstanceStatusesMeasuresRequest(string domain)
    {
        Keys = "instance_statuses";
        InstanceStatuses = new InstanceDomain
        {
            Domain = domain
        };
    }
}
