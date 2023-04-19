namespace CiT.Core.Entities.Measures;

/// <inheritdoc />
public class InstanceFollowersMeasuresRequest : MeasuresRequestBase
{
    /// <summary>
    ///     Constructs an InstanceFollowersMeasuresRequest using the provided domain to set the <see cref="InstanceDomain" />.
    /// </summary>
    /// <param name="domain">The remote domain for the measures request.</param>
    public InstanceFollowersMeasuresRequest(string domain)
    {
        Keys = "instance_followers";
        InstanceFollowers = new InstanceDomain
        {
            Domain = domain
        };
    }
    /// <summary>
    ///     The remote domain for the measures request.
    /// </summary>
    [JsonProperty("instance_followers")]
    public InstanceDomain? InstanceFollowers { get; set; }
}
