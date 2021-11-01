using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Database
{
    public class AreawaDbContextFactory : IDesignTimeDbContextFactory<AreawaDbContext>
    {
        public AreawaDbContext CreateDbContext(string[] args)
        {
            #region key vault
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

            KeyVaultSecret secret= client.GetSecret("dbconnectionstring");

            string secretValue = secret.Value;
            #endregion
            
            
            var optionsBuilder = new DbContextOptionsBuilder<AreawaDbContext>();
            optionsBuilder.UseSqlServer(secretValue);

            return new AreawaDbContext(optionsBuilder.Options);
        }
    }
}