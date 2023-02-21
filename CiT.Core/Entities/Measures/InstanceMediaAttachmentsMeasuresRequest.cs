using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

public class InstanceMediaAttachmentsMeasuresRequest : MeasuresRequestBase
{
    [JsonProperty("instance_media_attachments")] public InstanceDomain? InstanceMediaAttachments { get; set; }
    public InstanceMediaAttachmentsMeasuresRequest() { }
    public InstanceMediaAttachmentsMeasuresRequest(string domain)
    {
        Keys = "instance_media_attachments";
        InstanceMediaAttachments = new InstanceDomain
        {
            Domain = domain
        };
    }
}
