using CiT.Common.Attributes;

namespace CiT.Core.Entities;

/// <summary>
///     A object representing the instance configuration.
/// </summary>
public class InstanceConfiguration : ConfigurationBase
{
    /// <summary>
    ///     The API access token for the configured instance.
    /// </summary>
    [ConfigRequired]
    public string? AccessToken { get; set; }
    /// <summary>
    ///     The URL of the configured instance.
    /// </summary>
    [ConfigRequired]
    public string? Url { get; set; }
}
