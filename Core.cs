using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Peco
{
    internal static class Core
    {
        private static Process? _singBox = null;

        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole (uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        private static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(HandlerRoutine? Handler, bool Add);

        private delegate bool HandlerRoutine(uint dwControlType);

        private const uint CTRL_C_EVENT = 0;

        public static bool SUCCESS = true;

        public static void TurnOn()
        {
            if (!IsRequiredFilesExist())
            {
                SUCCESS = false;
                return;
            }

            /*
            if (Settings.Mode == Settings.TUN_MODE)
            {
                bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

                if (!isAdmin)
                {
                    Alert.Warning("TUN MODE: RELAUNCH APP AS ADMIN");
                    SUCCESS = false;
                    return;
                }
            }
            */

            Config.Validate();

            if (!Config.Good)
            {
                Alert.InvalidConfigFormat();
                SUCCESS = false;
                return;
            }

            _singBox = new Process();
            _singBox.StartInfo.FileName = Settings.CorePath;
            _singBox.StartInfo.Arguments = $"run -c {Settings.ConfigPath}";
            _singBox.StartInfo.CreateNoWindow = true;
            _singBox.StartInfo.UseShellExecute = false;

            _singBox.Start();

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

            _singBox?.Dispose();

            _singBox = null;
        }

        private static bool IsRequiredFilesExist()
        {
            if (!File.Exists(Settings.CorePath))
            {
                Alert.Error("sing-box.exe must be in the same folder as the app");
                return false;
            }

            if (!File.Exists(Settings.ConfigPath))
            {
                Alert.Error("No config selected");
                return false;
            }

            return true;
        }
    }
}