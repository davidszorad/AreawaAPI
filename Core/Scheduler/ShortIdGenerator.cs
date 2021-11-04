using System;
using System.Linq;

namespace Core.Scheduler
{
    internal static class ShortIdGenerator
    {
        private static Random random = new();
        
        public static string Generate()
        {
            return $"{DateTime.UtcNow.Year}-{DateTime.UtcNow.Month:MM}-{DateTime.UtcNow.Date:dd}-" +
                   $"{DateTime.UtcNow.Hour:hh}-{DateTime.UtcNow.Minute:mm}-" +
                   $"{GenerateRandomString(4)}";
        }
        
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}