namespace CiT.Core.Entities;

/// <summary>
///     An object representing a blocked IP address.
/// </summary>
public class BlockedIpAddress : BlockObjectBase
{
    /// <summary>
    ///     The blocked IP address.
    /// </summary>
    [JsonProperty("ip")]
    public string? Address { get; set; }
    /// <summary>
    ///     A string containing information about the current object.
    /// </summary>
    public string AddressInfo =>
        $"IP Address: {Address}\n" +
        $"Created At: {CreatedAt}\n" +
        $"Expires At: {ExpiresAt}\n" +
        $"Severity: {Severity}\n" +
        $"Comment: {Comment}";
    /// <summary>
    ///     A comment on the IP address block/ban..
    /// </summary>
    [JsonProperty("comment")]
    public string? Comment { get; set; }
    /// <summary>
    ///     The expiration date for the IP address block/ban..
    /// </summary>
    [JsonProperty("expires_at")]
    public DateTime? ExpiresAt { get; set; }
    /// <summary>
    ///     The severity of the block/ban of the IP address.
    /// </summary>
    [JsonProperty("severity")]
    public string? Severity { get; set; }
    /// <inheritdoc />
    public override string ToString() => $"{Address}";
}
