using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

/// <inheritdoc />
public class InstanceStatusesMeasuresRequest : MeasuresRequestBase
{
    /// <summary>
    ///     Constructs an InstanceStatusesMeasuresRequest object using the provided domain to set the
    ///     <see cref="InstanceDomain" />.
    /// </summary>
    /// <param name="domain"></param>
    public InstanceStatusesMeasuresRequest(string domain)
    {
        Keys = "instance_statuses";
        InstanceStatuses = new InstanceDomain
        {
            Domain = domain
        };
    }
    /// <summary>
    ///     The remote domain for the measures request.
    /// </summary>
    [JsonProperty("instance_statuses")]
    public InstanceDomain? InstanceStatuses { get; set; }
}
