using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CiT.Core.Entities;

public class BlockedEmailDomain : BlockObjectBase
{
    [JsonProperty("domain")] public string? Domain { get; set; }
    [JsonProperty("history")] public List<BlockedEmailDomainHistory>? History { get; set; }
    /// <inheritdoc />
    public override string ToString() => $"{Domain}";
    public string DomainInfo =>
        $"Domain: {Domain}\n" +
        $"Created At: {CreatedAt}";
}
public class BlockedEmailDomainHistory
{
    private dynamic _day;
    [JsonProperty("day")]
    public dynamic Day
    {
        get => _day;
        set
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(int.Parse(value));
            _day = dateTimeOffset.UtcDateTime;
        }
    }
    [JsonProperty("accounts")] public int Accounts { get; set; }
    [JsonProperty("uses")] public int Uses { get; set; }
}
