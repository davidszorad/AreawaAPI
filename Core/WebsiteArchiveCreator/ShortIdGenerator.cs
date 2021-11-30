using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Core.UnitTests")]

namespace Core.WebsiteArchiveCreator
{
    internal static class ShortIdGenerator
    {
        private static readonly Random Random = new();
        
        public static string Generate()
        {
            return $"{DateTime.UtcNow.Year}-{DateTime.UtcNow:MM}-{DateTime.UtcNow:dd}-" +
                   $"{DateTime.UtcNow:HH}-{DateTime.UtcNow:mm}-" +
                   $"{GenerateRandomString(4)}";
        }
        
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}