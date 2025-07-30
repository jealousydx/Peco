using System.Security.Principal;

namespace Peco.app
{
    internal static class Program
    {
        private static Mutex? _mutex;
        private static bool _isMutexFree;

        private static bool _isElevated;
        public static bool IsElevated => _isElevated;

        [STAThread]
        static void Main()
        {
            _isElevated = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            
            _mutex = new Mutex(true, "Global\\Peco", out _isMutexFree);
            if (!_isMutexFree)
            {
                Alert.Error("Peco.exe is already running");
                return;
            }

            ApplicationConfiguration.Initialize();

            App.Run();
        }

        public static void DisposeMutex()
        {
            if (_mutex != null)
            {
                _mutex.ReleaseMutex();
                _mutex.Dispose();
                _mutex = null;
            }
        }
    }
}