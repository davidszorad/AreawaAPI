using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("Core.UnitTests")]

namespace Core.ChangeTracker.Extensions;

internal static class HtmlExtensions
{
    public static string StripHead(this string htmlContent)
    {
        if (!htmlContent.Contains("<head>") || !htmlContent.Contains("</head>"))
        {
            return htmlContent;
        }
        
        var part1 = htmlContent.Split("<head>")[0];
        var part2 = htmlContent.Split("</head>")[1];

        return $"{part1}<HEAD>{part2}";
    }
    
    public static string StripScripts(this string htmlContent)
    {
        var result = htmlContent;
        
        foreach (Match match in Regex.Matches(htmlContent, @"(?<=<script>)(.*?)(?=</script>)", RegexOptions.IgnoreCase)) // ?<= and ?= positive / negative lookahead
        {
            if (match.Success)
            {
                result = result.Replace($"<script>{match.Value}</script>", string.Empty, StringComparison.Ordinal);
            }
        }

        return result;
    }
    
    public static string StripStyles(this string htmlContent)
    {
        var result = htmlContent;
        
        foreach (Match match in Regex.Matches(htmlContent, @"(?<=<style>)(.*?)(?=</style>)", RegexOptions.IgnoreCase))
        {
            if (match.Success)
            {
                result = result.Replace($"<style>{match.Value}</style>", string.Empty, StringComparison.Ordinal);
            }
        }

        return result;
    }
}