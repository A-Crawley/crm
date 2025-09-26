using System.Text.RegularExpressions;

namespace Domain.Extensions;

public static partial class StringExtensions
{ 
    [GeneratedRegex("(?<!^)([A-Z0-1])")]
    private static partial Regex CaptureStringForSnakeCaseRegex();
    
    public static string? ToSnakeCase(this string? text)
    {
        if (string.IsNullOrEmpty(text)) return text;
        return CaptureStringForSnakeCaseRegex().Replace(text, "_$1").ToLower();
    }
}