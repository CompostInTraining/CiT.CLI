using System;
using Newtonsoft.Json;

namespace CiT.Core.Entities;

public class BlockedIpAddress : BlockObjectBase
{
    [JsonProperty("ip")] public string? Address { get; set; }
    [JsonProperty("comment")] public string? Comment { get; set; }
    [JsonProperty("expires_at")] public DateTime? ExpiresAt { get; set; }
    [JsonProperty("severity")] public string? Severity { get; set; }
    /// <inheritdoc />
    public override string ToString() => $"{Address}";
    public string AddressInfo =>
        $"IP Address: {Address}\n" +
        $"Created At: {CreatedAt}\n" +
        $"Expires At: {ExpiresAt}\n" +
        $"Severity: {Severity}\n" +
        $"Comment: {Comment}";
}