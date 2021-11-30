using System.Runtime.InteropServices;

namespace Infrastructure;

public static class RuntimeInfoService
{
    public static bool IsMac()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    }
}