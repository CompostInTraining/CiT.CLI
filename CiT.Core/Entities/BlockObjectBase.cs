using System;
using Newtonsoft.Json;

namespace CiT.Core.Entities;

/// <summary>
///     A base object for blocks. Contains common properties.
/// </summary>
public class BlockObjectBase
{
    /// <summary>
    ///     A timestamp representing the creation date of the current object.
    /// </summary>
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    /// <summary>
    ///     The ID representing the current object.
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }
}
