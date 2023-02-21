using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

public class MeasuresRequestBase
{
    [JsonProperty("keys")] public string? Keys { get; set; }
    [JsonProperty("start_at")] public DateOnly? StartAt { get; set; }
    [JsonProperty("end_at")] public DateOnly? EndAt { get; set; }
    public MeasuresRequestBase()
    {
        var now = DateTime.Today;
        EndAt = DateOnly.FromDateTime(now);
        StartAt = DateOnly.FromDateTime(now.AddDays(-7));
    }
    public StringContent ToStringContent()
        => ToStringContent(
            Encoding.UTF8,
            new MediaTypeHeaderValue(MediaTypeNames.Application.Json)
        );
    public StringContent ToStringContent(Encoding encoding, MediaTypeHeaderValue headerValue)
    {
        string json = JsonConvert.SerializeObject(this);
        return new StringContent(json, encoding, headerValue);
    }
}
public class InstanceDomain
{
    [JsonProperty("domain")] public string? Domain { get; set; }
}