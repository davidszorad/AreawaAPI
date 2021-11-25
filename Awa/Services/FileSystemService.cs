using System.Runtime.CompilerServices;
using Configuration;

[assembly: InternalsVisibleTo("Awa.UnitTests")]

namespace Awa;

internal static class FileSystemService
{
    public static string GetProfileFolder()
    {
        var rootFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var directory = Path.Combine(rootFolderPath, ConfigurationConstants.ProfileFolder);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        return directory;
    }
    
    public static async Task SaveTextAsync(string text, string fileName, CancellationToken cancellationToken = default)
    {
        await File.WriteAllTextAsync(Path.Combine(GetProfileFolder(), $"{fileName}.txt"), text, cancellationToken);
    }
    
    public static async Task<string> ReadTextAsync(string fileName, CancellationToken cancellationToken = default)
    {
        var filePath = Path.Combine(GetProfileFolder(), $"{fileName}.txt");
        if (!File.Exists(filePath))
        {
            return string.Empty;
        }
        
        return await File.ReadAllTextAsync(filePath, cancellationToken);
    }

    public static void DeleteFile(string fileName)
    {
        File.Delete(Path.Combine(GetProfileFolder(), $"{fileName}.txt"));
    }
}