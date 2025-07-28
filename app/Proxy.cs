using System.Runtime.InteropServices;
using Microsoft.Win32;

public static class Proxy
{
    private const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
    private const int INTERNET_OPTION_REFRESH = 37;

    [DllImport("wininet.dll")]
    private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);

    public static void Disable()
    {
        const string internetSettingsKey = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";

        using (var key = Registry.CurrentUser.OpenSubKey(internetSettingsKey, writable: true))
        {
            key?.SetValue("ProxyEnable", 0, RegistryValueKind.DWord);
        }

        InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
        InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
    }
}