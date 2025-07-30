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

        public static void ShowLogForm()
        {
            _logForm.Show();
        }

        public static void Exit()
        {
            ConfirmExiting();

            Core.TurnOff();
            Settings.Save();

            Application.Exit();
        }

        public static bool Exiting => _isExiting;

        private static void ConfirmExiting() => _isExiting = true;
    }
}