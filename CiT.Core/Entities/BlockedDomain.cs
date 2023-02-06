using System;

namespace CiT.Core.Entities;

public class BlockedDomain
{
    public string? Id { get; set; }
    public string? Domain { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Severity { get; set; }
    public bool RejectMedia { get; set; }
    public bool RejectReports { get; set; }
    public string? PrivateComment { get; set; }
    public string? PublicComment { get; set; }
    public bool Obfuscate { get; set; }
    /// <inheritdoc />
    public override string ToString() => $"{Domain}";
}
