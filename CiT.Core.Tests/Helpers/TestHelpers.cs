using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#pragma warning disable CS8604
#pragma warning disable CS8600
#pragma warning disable CS8602
namespace CiT.Core.Tests.Helpers;

public static class EntityTests
{
    public static bool HasMethod(this object obj, string methodName)
    {
        Type type = obj.GetType();
        try
        {
            return type.GetMethod(methodName) != null;
        }
        catch (AmbiguousMatchException)
        {
            return true;
        }
    }
    public static bool HasProperty(this object obj, string propertyName)
    {
        Type type = obj.GetType();
        return type.GetProperty(propertyName) != null;
    }
    public static int PropertyCount(this object obj)
    {
        Type type = obj.GetType();
        return type.GetProperties().Length;
    }
    public static T SetProperties<T>(T domainObject, bool recursive = false)
    {
        var props = domainObject.GetType().GetProperties();
        foreach (var prop in props)
        {
            Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            try
            {
                object propObj = null;
                object data;
                switch (propType.Name.ToLower())
                {
                    case "string":
                        data = "test";
                        break;
                    case "int":
                    case "int32":
                        data = 2;
                        break;
                    case "datetime":
                        data = DateTime.Now;
                        break;
                    case "bool":
                    case "boolean":
                        data = true;
                        break;
                    case "decimal":
                        data = decimal.Parse("1.23");
                        break;
                    case "double":
                        data = double.Parse("3.21");
                        break;
                    default:
                        if (propType.IsInterface)
                        {
                            propObj = null;
                        }
                        else if (propType.IsArray)
                        {
                            var elementType = propType.GetElementType();
                            propObj = Array.CreateInstance(elementType, 1);
                        }
                        else
                        {
                            var ctr = propType.GetConstructors()[0];
                            propObj = ctr.GetParameters().Any() ? null : Activator.CreateInstance(propType);
                        }
                        data = propObj;
                        break;
                }
                if (data != null && prop.CanWrite)
                {
                    prop.SetValue(domainObject, data);
                    Assert.AreEqual(data, prop.GetValue(domainObject));
                }
                if (recursive && propObj != null)
                {
                    if (propType.IsGenericType)
                    {
                        Type myListElementType = propType.GetGenericArguments().Single();
                        propObj = Activator.CreateInstance(myListElementType);
                    }
                    SetProperties(propObj, recursive);
                }
            }
            catch (Exception ex)
            {
                var msg = string.Format(
                    "Error creating instance of {0} because: {1}",
                    propType.Name, ex.Message);
                Assert.IsNotNull(prop.GetValue(domainObject), msg);
            }
        }
        return domainObject;
    }
}
public static class RandomString
{
    public static string New(int length = 32)
    {
        const string src = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var sb = new StringBuilder();
        Random RNG = new Random();
        for (var i = 0; i < length; i++)
        {
            var c = src[RNG.Next(0, src.Length)];
            sb.Append(c);
        }
        return sb.ToString();
    }
}
