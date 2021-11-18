namespace Configuration
{
    public static class ConfigurationConstants
    {
        internal const string KeyVaultAddress = "https://areawa.vault.azure.net/";
        internal const string DbConnectionString = "dbconnectionstring";
        
        public const string UploadsRoot = "uploads";
        public const string AzureStorageConnectionString = "AzureStorageConnectionString";
        public const string QueueIncomingProcessorRequests = "incoming-processor-requests";
        public const int MaxDequeueCount = 5;
    }
}