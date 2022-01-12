namespace Configuration;

public static class ConfigurationConstants
{
    internal const string KeyVaultAddress = "https://areawa.vault.azure.net/";
    internal const string DbConnectionString = "dbconnectionstring";
        
    public const string ProfileFolder = "areawa";
    public const string AzureStorageConnectionString = "AzureStorageConnectionString";
    public const int MaxDequeueCount = 5;
    public const string FileNameWithApiKey = "identity";
    public const string ApiRootUrl = "https://areawaapi.azurewebsites.net";
    public const string ApiUrlWebsiteArchiveCreate = "api/wa/create";
    public const string ApiUrlWebsiteArchiveUpload = "api/wa/upload";
    public const string WatchDogIncomingQueue = "watchdog-incoming";
    public const string SenderEmail = "areawa@dev-trips.com";
    public const string SenderName = "Areawa";
    public const string SendGridApiKey = "SendGridApiKey";
}