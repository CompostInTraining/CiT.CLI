using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiT.Core.Entities;

/// <summary>
///     An object representing a blocked email domain.
/// </summary>
public class BlockedEmailDomain : BlockObjectBase
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
        $"Created At: {CreatedAt}";
    /// <summary>
    ///     A list of BlockedEmailDomainHistory representing information about the history of this domain name's use.
    /// </summary>
    [JsonProperty("history")]
    public List<BlockedEmailDomainHistory>? History { get; set; }
    /// <inheritdoc />
    public override string ToString() => $"{Domain}";
}
/// <summary>
///     An object representing use history for a blocked email domain.
/// </summary>
public class BlockedEmailDomainHistory
{
    private dynamic? _day;
    /// <summary>
    ///     The counted accounts signup attempts using this email domain within this day.
    /// </summary>
    [JsonProperty("accounts")]
    public int Accounts { get; set; }
    /// <summary>
    ///     The day for this history.
    /// </summary>
    [JsonProperty("day")]
    public dynamic? Day
    {
        get => _day;
        set
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(int.Parse(value));
            _day = dateTimeOffset.UtcDateTime;
        }
    }
    /// <summary>
    ///     The counted IP signup attempts of thi email domain within this day.
    /// </summary>
    [JsonProperty("uses")]
    public int Uses { get; set; }
}
