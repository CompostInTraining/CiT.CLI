using System;
using System.Reflection;

namespace CiT.Core.Validation;

public static class ConfigurationValidation
{
    // https://stackoverflow.com/questions/22683040/how-to-check-all-properties-of-an-object-whether-null-or-empty/22683141#22683141
    public static bool IsAnyNullOrEmpty(this object obj)
    {
        foreach (PropertyInfo pi in obj.GetType().GetProperties())
        {
            if (Attribute.IsDefined(pi, typeof(ConfigRequiredAttribute)) ||
                Attribute.IsDefined(obj.GetType(), typeof(ConfigRequiredAttribute)))
            {
                var value = pi.GetValue(obj);
                if (value is null)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
public class ConfigRequiredAttribute : Attribute { }