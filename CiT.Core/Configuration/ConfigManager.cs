using System.Configuration;
using CiT.Core.Entities;
using CiT.Core.Exceptions;
using CiT.Core.Validation;
using Microsoft.Extensions.Configuration;

namespace CiT.Core.Configuration;

public interface IConfigManager
{
    IConfiguration Configuration { get; set; }
    InstanceConfiguration Instance { get; set; }
    string GetConfigValue(string key);
}
public class ConfigManager : IConfigManager
{
    public IConfiguration Configuration { get; set; }
    public InstanceConfiguration Instance { get; set; }
    public ConfigManager(
        IConfiguration configuration,
        InstanceConfiguration instance)
    {
        Configuration = configuration;
        Instance = instance;

        if (!Instance.IsValid())
        {
            throw new InvalidConfigurationException();
        }
    }
    public string GetConfigValue(string key)
    {
        string? value = Configuration[key];
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ConfigurationErrorsException(
                $"An invalid key was provided: key: {key}");
        }
        return value;
    }
}
public abstract class ConfigurationBase
{
    public bool IsValid() => !this.IsAnyNullOrEmpty();
}
