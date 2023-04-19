using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

/// <summary>
///     An object representing the current measures request.
/// </summary>
public class MeasuresRequestBase
{
    /// <summary>
    ///     Constructs a measure request object, initializing the time period to be the last seven days.
    /// </summary>
    public MeasuresRequestBase()
    {
        var now = DateTime.Today;
        EndAt = DateOnly.FromDateTime(now);
        StartAt = DateOnly.FromDateTime(now.AddDays(-7));
    }
    /// <summary>
    ///     The end date for the measure request period.
    /// </summary>
    [JsonProperty("end_at")]
    public DateOnly? EndAt { get; set; }
    /// <summary>
    ///     The unique keystring for the current measure request.
    /// </summary>
    [JsonProperty("keys")]
    public string? Keys { get; set; }
    /// <summary>
    ///     The start date for the measure request period.
    /// </summary>
    [JsonProperty("start_at")]
    public DateOnly? StartAt { get; set; }
    /// <summary>
    ///     Convert this object to a JSON-serialized StringContent, using a common Encoding and MediaTypeHeaderValues.
    /// </summary>
    /// <returns>A StringContent object representing the current object.</returns>
    public StringContent ToStringContent()
        => ToStringContent(
            Encoding.UTF8,
            new MediaTypeHeaderValue(MediaTypeNames.Application.Json)
        );
    /// <summary>
    ///     Conver this object to a JSON-serialized StringContent.
    /// </summary>
    /// <param name="encoding">The Encoding to use.</param>
    /// <param name="headerValue">The MediaTypeHeaderValue to set.</param>
    /// <returns></returns>
    public StringContent ToStringContent(Encoding encoding, MediaTypeHeaderValue headerValue)
    {
        string json = JsonConvert.SerializeObject(this);
        return new StringContent(json, encoding, headerValue);
    }
}
public class InstanceDomain
{
    /// <summary>
    ///     The remote domain for the measure request.
    /// </summary>
    [JsonProperty("domain")]
    public string? Domain { get; set; }
}
