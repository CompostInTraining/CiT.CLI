using System;

namespace CiT.Core.Extensions;

public static class StringExtensions
{
    public static bool IsHelp(this string a)
		=> string.Equals(a, "help", StringComparison.OrdinalIgnoreCase);
}
