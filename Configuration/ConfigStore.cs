using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Configuration
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
            var client = new SecretClient(new Uri(ConfigurationConstants.KeyVaultAddress), new DefaultAzureCredential(), options);

            KeyVaultSecret secret = client.GetSecret(key);

            return secret.Value;
        }

        public static string GetDbConnectionString()
        {
            return GetValue(ConfigurationConstants.DbConnectionString);
        }
    }
}