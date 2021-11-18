namespace Configuration
{
    public static class ConfigurationConstants
    {
        internal const string KeyVaultAddress = "https://areawa.vault.azure.net/";
        internal const string DbConnectionString = "dbconnectionstring";
        
        public const string UploadsRoot = "uploads";
        public const string AzureStorageConnectionString = "StorageConnectionString";
        public const string QueueIncomingProcessorRequests = "QueueIncomingProcessorRequests";
        public const string PoisonQueueIncomingProcessorRequests = "PoisonQueueIncomingProcessorRequests";
    }
}