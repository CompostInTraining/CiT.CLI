using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

/// <inheritdoc />
public class InstanceReportsMeasuresRequest : MeasuresRequestBase
{
    /// <summary>
    ///     Constructs an InstanceReportsMeasuresRequest object using the provided domain to set the
    ///     <see cref="InstanceDomain" />.
    /// </summary>
    /// <param name="domain"></param>
    public InstanceReportsMeasuresRequest(string domain)
    {
        Keys = "instance_reports";
        InstanceReports = new InstanceDomain
        {
            Domain = domain
        };
    }
    /// <summary>
    ///     The remote domain for the measures request.
    /// </summary>
    [JsonProperty("instance_reports")]
    public InstanceDomain? InstanceReports { get; set; }
}
