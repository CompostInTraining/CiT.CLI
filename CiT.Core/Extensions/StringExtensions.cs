namespace CiT.Core.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Checks whether the string is equal to "help", ignoring case.
    /// </summary>
    /// <param name="a">The string to test.</param>
    /// <returns>True if the provided string equals "help", otherwise False.</returns>
    public static bool IsHelp(this string a)
		=> string.Equals(a, "help", StringComparison.OrdinalIgnoreCase);
}
