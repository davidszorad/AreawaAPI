﻿using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;

namespace Infrastructure
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
            var client = new SecretClient(new Uri(InfrastructureConstants.KEY_VAULT_ADDRESS), new DefaultAzureCredential(), options);

            KeyVaultSecret secret = client.GetSecret(key);

            return secret.Value;
        }
    }
}
