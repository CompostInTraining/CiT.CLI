using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiT.Core.Entities.Measures;

public class MeasuresResponse
{
    public List<MeasuresResponseObject>? Response { get; init; }
}
public class MeasuresResponseObject
{
    [JsonProperty("key")] public string? Key { get; set; }
    [JsonProperty("total")] public long Total { get; set; }
    [JsonProperty("previous_total")] public long PreviousTotal { get; set; }
    [JsonProperty("data")] public List<MeasuresResponseDataObject>? Data { get; set; }
    [JsonProperty("human_value")] public string? HumanValue { get; set; }
}
public class MeasuresResponseDataObject
{
    public string? Date { get; set; }
    public string? Value { get; set; }
}
