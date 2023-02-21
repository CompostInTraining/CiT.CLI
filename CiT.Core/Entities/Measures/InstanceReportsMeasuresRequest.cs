using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

public class InstanceReportsMeasuresRequest : MeasuresRequestBase
{
    [JsonProperty("instance_reports")] public InstanceDomain? InstanceReports { get; set; }
    public InstanceReportsMeasuresRequest() { }
    public InstanceReportsMeasuresRequest(string domain)
    {
        Keys = "instance_reports";
        InstanceReports = new InstanceDomain
        {
            Domain = domain
        };
    }
}
