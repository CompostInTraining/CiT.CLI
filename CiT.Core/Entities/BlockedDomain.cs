using System;
using Newtonsoft.Json;

namespace CiT.Core.Entities;

public class BlockedDomain
{
    [JsonProperty("id")] public string? Id { get; set; }
    [JsonProperty("domain")] public string? Domain { get; set; }
    [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }
    [JsonProperty("severity")] public string? Severity { get; set; }
    [JsonProperty("reject_media")] public bool RejectMedia { get; set; }
    [JsonProperty("reject_reports")] public bool RejectReports { get; set; }
    [JsonProperty("private_comment")] public string? PrivateComment { get; set; }
    [JsonProperty("public_comment")] public string? PublicComment { get; set; }
    [JsonProperty("obfuscate")] public bool Obfuscate { get; set; }
    /// <inheritdoc />
    public override string ToString() => $"{Domain}";
    public string DomainInfo =>
        $"Domain: {Domain}\n" +
        $"Created At: {CreatedAt}\n" +
        $"Public Comment: {PublicComment}\n" +
        $"Private Comment: {PrivateComment}\n" +
        $"Severity: {Severity}";
}
