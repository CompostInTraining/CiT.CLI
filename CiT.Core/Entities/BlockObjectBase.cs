using System;
using Newtonsoft.Json;

namespace CiT.Core.Entities;

public class BlockObjectBase
{
    [JsonProperty("id")] public string? Id { get; set; }
    [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }
}
