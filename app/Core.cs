using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Peco.app
{
    internal static class Core
    {
        private static Process? _singBox = null;
        public const string ADAPTER_NAME = "sing-tun Tunnel";

        public static bool SUCCESS = true;

        public static void TurnOn()
        {
            if (Settings.Mode == Settings.TUN_MODE)
            {
                bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

                if (!isAdmin)
                {
                    Alert.TunModeError();
                    SUCCESS = false;
                    return;
                }
            }

            if (!IsRequiredFilesExist())
            {
                SUCCESS = false;
                return;
            }

            Config.Validate();

            if (!Config.Good)
            {
                SUCCESS = false;
                return;
            }

            Config.Build();

            var startInfo = new ProcessStartInfo
            {
                FileName = Settings.CorePath,
                Arguments = $"run -c {Settings.ConfigPath}",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true
            };

            _singBox = new Process { StartInfo = startInfo };
            _singBox.ErrorDataReceived += Log.HandleOutputData;
            _singBox.Start();
            _singBox.BeginErrorReadLine();

            SUCCESS = true;
        }

        public static void TurnOff()
        {
            if (_singBox == null)
            {
                return;
            }

            SetConsoleCtrlHandler(null, true);
            AttachConsole((uint)_singBox.Id);
            GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0);

            _singBox.WaitForExit();

            FreeConsole();
            SetConsoleCtrlHandler(null, false);

            _singBox.Dispose();
            _singBox = null;
        }

        private static bool IsRequiredFilesExist()
        {
            if (!File.Exists(Settings.CorePath))
            {
                Alert.Error("sing-box.exe must be in the <core/> folder");
                return false;
            }

            if (!File.Exists(Settings.ConfigPath))
            {
                Alert.Error("No config selected");
                return false;
            }

            return true;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        private static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(HandlerRoutine? Handler, bool Add);

        private delegate bool HandlerRoutine(uint dwControlType);

        private const uint CTRL_C_EVENT = 0;
    }
}