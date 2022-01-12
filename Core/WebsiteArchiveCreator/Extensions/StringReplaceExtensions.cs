namespace Core.WebsiteArchiveCreator;

internal static class StringReplaceExtensions
{
    public static string ReplaceNotAllowedChars(this string text)
    {
        return text
            .Trim()
            .Replace(" ", "-")
            .Replace("#", "")
            .Replace("@", "")
            .Replace("~", "")
            .Replace("!", "")
            .Replace("?", "")
            .Replace(":", "")
            .Replace(";", "")
            .Replace("^", "")
            .Replace("&", "")
            .Replace("%", "")
            .Replace("$", "")
            .Replace(",", "");
    }
}