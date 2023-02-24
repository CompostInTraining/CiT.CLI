using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

/// <inheritdoc />
public class InstanceAccountsMeasuresRequest : MeasuresRequestBase
{
    /// <summary>
    ///     Constructs an InstanceAccountsMeasureRequest using the provided domain to set the <see cref="InstanceDomain" />
    /// </summary>
    /// <param name="domain">The remote domain for the measures request.</param>
    public InstanceAccountsMeasuresRequest(string domain)
    {
        Keys = "instance_accounts";
        InstanceAccounts = new InstanceDomain
        {
            Domain = domain
        };
    }
    /// <summary>
    ///     The remote domain for the measures request.
    /// </summary>
    [JsonProperty("instance_accounts")]
    public InstanceDomain? InstanceAccounts { get; set; }
}
