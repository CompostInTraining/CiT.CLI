using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

/// <inheritdoc />
public class InstanceMediaAttachmentsMeasuresRequest : MeasuresRequestBase
{
    /// <summary>
    ///     Constructs an InstanceMediaAttachmentsMeasuresRequest using the provided domain to set the
    ///     <see cref="InstanceDomain" />.
    /// </summary>
    /// <param name="domain">The remote domain for the measures request.</param>
    public InstanceMediaAttachmentsMeasuresRequest(string domain)
    {
        Keys = "instance_media_attachments";
        InstanceMediaAttachments = new InstanceDomain
        {
            Domain = domain
        };
    }
    /// <summary>
    ///     The remote domain for the measures request.
    /// </summary>
    [JsonProperty("instance_media_attachments")]
    public InstanceDomain? InstanceMediaAttachments { get; set; }
}
