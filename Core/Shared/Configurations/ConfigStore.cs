using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Core.Shared
{
    public static class ConfigStore
    {
        public static string GetValue(string key)
        {
            var options = new SecretClientOptions
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };
            var client = new SecretClient(new Uri("https://areawa.vault.azure.net/"), new DefaultAzureCredential(), options);

            KeyVaultSecret secret= client.GetSecret(key);

            return secret.Value;
        }
    }
}