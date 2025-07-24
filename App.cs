namespace Peco
{
    internal partial class App
    {
        private bool _isExiting;

        private readonly Form _mainForm;
        private readonly Form _logForm;

        public App()
        {
            Settings.Init();
            Log.Init();

            _mainForm = new MainForm(this);
            _logForm = new LogForm(this);

            _isExiting = false;
        }
        public void Run()
        {
            Application.Run(_mainForm);
        }

        public void ShowLogForm()
        {
            _logForm.Show();
        }

        private void CloseAllForms()
        {
            _mainForm.Close();
            _logForm.Close();
        }

        public void Exit()
        {
            Core.TurnOff();
            Settings.Save();

            ConfirmExiting();
            CloseAllForms();

            Application.Exit();
        }

        public bool IsExiting() => _isExiting;

        private void ConfirmExiting() => _isExiting = true;
    }
}
