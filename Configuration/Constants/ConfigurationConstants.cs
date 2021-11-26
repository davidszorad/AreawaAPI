namespace Configuration
{
    public static class ConfigurationConstants
    {
        internal const string KeyVaultAddress = "https://areawa.vault.azure.net/";
        internal const string DbConnectionString = "dbconnectionstring";

        public const string ProfileFolder = "areawa";
        public const string AzureStorageConnectionString = "AzureStorageConnectionString";
        public const int MaxDequeueCount = 5;
        public const string FileNameWithApiKey = "identity";
        //public const string ApiRootUrl = "https://areawaapi.azurewebsites.net";
        public const string ApiRootUrl = "https://localhost:5001";
        public const string ApiScreenshotUrl = "api/website-archive/upload";
    }
}