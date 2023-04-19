namespace CiT.Core.Entities.Measures;

/// <summary>
///     An object representing a Measures API response.
/// </summary>
public class MeasuresResponse
{
    /// <summary>
    ///     A property containing the response(s).
    /// </summary>
    public List<MeasuresResponseObject>? Response { get; init; }
}
/// <summary>
///     An object representing an individual Measures API response object.
/// </summary>
public class MeasuresResponseObject
{
    /// <summary>
    ///     The data available for the requested measure, split into daily buckets.
    /// </summary>
    [JsonProperty("data")]
    public List<MeasuresResponseDataObject>? Data { get; set; }
    /// <summary>
    ///     A human-readable formatted value for this data item.
    /// </summary>
    [JsonProperty("human_value")]
    public string? HumanValue { get; set; }
    /// <summary>
    ///     The unique keystring for the requested measure.
    /// </summary>
    [JsonProperty("key")]
    public string? Key { get; set; }
    /// <summary>
    ///     The numeric total associated with the requested measure, in the previous period.
    /// </summary>
    [JsonProperty("previous_total")]
    public long PreviousTotal { get; set; }
    /// <summary>
    ///     The numeric total associated with the requested measure.
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }
}
/// <summary>
///     An object representing a daily bucket of data for the requested measure.
/// </summary>
public class MeasuresResponseDataObject
{
    /// <summary>
    ///     Midnight on the requested day in the time period.
    /// </summary>
    public string? Date { get; set; }
    /// <summary>
    ///     The numeric value for the requested measure.
    /// </summary>
    public string? Value { get; set; }
}
