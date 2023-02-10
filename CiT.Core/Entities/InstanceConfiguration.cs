using CiT.Core.Configuration;
using CiT.Core.Validation;

namespace CiT.Core.Entities;

public class InstanceConfiguration : ConfigurationBase
{
    [ConfigRequired] public string? Url { get; set; }
    [ConfigRequired] public string? AccessToken { get; set; }
}
