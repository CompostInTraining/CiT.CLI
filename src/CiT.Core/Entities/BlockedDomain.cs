namespace CiT.Core.Entities;

/// <summary>
///     An object representing a blocked domain.
/// </summary>
public class BlockedDomain : BlockObjectBase
{
    /// <summary>
    ///     The blocked domain name.
    /// </summary>
    [JsonProperty("domain")]
    public string? Domain { get; set; }
    /// <summary>
    ///     A string containing information about the current object.
    /// </summary>
    public string DomainInfo =>
        $"Domain: {Domain}\n" +
        $"Created At: {CreatedAt}\n" +
        $"Public Comment: {PublicComment}\n" +
        $"Private Comment: {PrivateComment}\n" +
        $"Severity: {Severity}";
    /// <summary>
    ///     Whether to obfuscate the domain name.
    /// </summary>
    [JsonProperty("obfuscate")]
    public bool Obfuscate { get; set; }
    /// <summary>
    ///     A private comment on the domain block.
    /// </summary>
    [JsonProperty("private_comment")]
    public string? PrivateComment { get; set; }
    /// <summary>
    ///     A public comment on the domain block.
    /// </summary>
    [JsonProperty("public_comment")]
    public string? PublicComment { get; set; }
    /// <summary>
    ///     Whether to reject media from the domain.
    /// </summary>
    [JsonProperty("reject_media")]
    public bool RejectMedia { get; set; }
    /// <summary>
    ///     Whether to reject reports from the domain.
    /// </summary>
    [JsonProperty("reject_reports")]
    public bool RejectReports { get; set; }
    /// <summary>
    ///     The severity of the domain block.
    /// </summary>
    [JsonProperty("severity")]
    public string? Severity { get; set; }
    /// <inheritdoc />
    public override string ToString() => $"{Domain}";
}
