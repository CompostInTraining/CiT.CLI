using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

/// <inheritdoc />
public class InstanceFollowsMeasuresRequest : MeasuresRequestBase
{
    /// <summary>
    ///     Constructs an InstanceFollowsMeasuresRequest object using the provided domain to set the
    ///     <see cref="InstanceDomain" />.
    /// </summary>
    /// <param name="domain">The remote domain for the measures request.</param>
    public InstanceFollowsMeasuresRequest(string domain)
    {
        Keys = "instance_follows";
        InstanceFollows = new InstanceDomain
        {
            Domain = domain
        };
    }
    /// <summary>
    ///     The remote domain for the measures request.
    /// </summary>
    [JsonProperty("instance_follows")]
    public InstanceDomain? InstanceFollows { get; set; }
}
