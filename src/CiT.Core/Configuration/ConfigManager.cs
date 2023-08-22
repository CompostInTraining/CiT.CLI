using System.Configuration;
using CiT.Common.Exceptions;
using CiT.Common.Validations;
using Microsoft.Extensions.Configuration;

namespace CiT.Core.Configuration;

/// <summary>
///     The IConfigManager allows you to read config settings.
/// </summary>
public interface IConfigManager
{
    /// <summary>
    ///     The main configuration object.
    /// </summary>
    IConfiguration Configuration { get; set; }
    /// <summary>
    ///     Instance configuration.
    /// </summary>
    InstanceConfiguration Instance { get; set; }
    /// <summary>
    ///     Gets the requested key from the Configuration.
    /// </summary>
    /// <param name="key">The key to request from Configuration</param>
    /// <returns>The value of the requested key.</returns>
    /// <exception cref="ConfigurationErrorsException">Thrown when the requested key does not exist in the Configuration.</exception>
    string GetConfigValue(string key);
}
public class ConfigManager : IConfigManager
{
    /// <summary>
    ///     Constructs a ConfigManager with all Configuration objects.
    /// </summary>
    /// <param name="configuration">The main program Configuration</param>
    /// <param name="instance">The instance configuration.</param>
    /// <exception cref="InvalidConfigurationException">Thrown if any of the provided configurations objects are invalid.</exception>
    public ConfigManager(
        IConfiguration configuration)
    {
        Configuration = configuration;
        Instance = Configuration.GetSection("Instance").Get<InstanceConfiguration>() ?? throw new InvalidConfigurationException();

        if (!Instance.IsValid())
        {
            throw new InvalidConfigurationException();
        }
    }
    /// <inheritdoc />
    public IConfiguration Configuration { get; set; }
    /// <inheritdoc />
    public InstanceConfiguration Instance { get; set; }
    /// <inheritdoc />
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
/// <summary>
///     A base class for configuration objects to inherit the IsValid method.
/// </summary>
public abstract class ConfigurationBase
{
    /// <summary>
    ///     Tests whether all the required properties have been set in the configuration object.
    /// </summary>
    /// <returns>True if all required properties are set, otherwise False.</returns>
    public bool IsValid() => !this.IsAnyNullOrEmpty();
}
