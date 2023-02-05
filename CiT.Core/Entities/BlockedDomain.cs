using System;

namespace CiT.Core.Entities;

public class BlockedDomain
{
    public string Id { get; set; }
    public string Domain { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Severity { get; set; }
    public Boolean RejectMedia { get; set; }
    public Boolean RejectReports { get; set; }
    public string PrivateComment { get; set; }
    public string PublicComment { get; set; }
    public Boolean Obfuscate { get; set; }
    /// <inheritdoc />
    public override string ToString() => $"{Domain}";
}
