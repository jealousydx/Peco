using System.ComponentModel;
using System.Diagnostics;

namespace Peco.app
{
    internal static partial class App
    {
        private static readonly Form _mainForm;
        private static readonly Form _logForm;

        private static bool _isExiting = false;

        static App()
        {
            SingTunAdapter.Remove();
            Settings.Init();
            Log.Init();

            _mainForm = new MainForm();
            _logForm = new LogForm();
        }
        public static void Run()
        {
            Application.Run(_mainForm);
        }

        public static void Restart()
        {
            var appPath = Process.GetCurrentProcess().MainModule?.FileName;

            var startInfo = new ProcessStartInfo
            {
                FileName = appPath,
                UseShellExecute = true,
                Verb = "runas"
            };

            try
            {
                Process.Start(startInfo);
                Program.DisposeMutex();
            }
            catch (Win32Exception)
            {
                return;
            }

            Exit();
        }

        public static void Exit()
        {
            ConfirmExiting();

            Core.TurnOff();
            Settings.Save();

            Application.Exit();
        }

        public static void ShowLogForm()
        {
            _logForm.Show();
        }

        public static bool Exiting => _isExiting;

        private static void ConfirmExiting() => _isExiting = true;
    }
}