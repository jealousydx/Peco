namespace Peco
{
    internal partial class App
    {
        private bool _isExiting;

        private readonly Form _mainForm;

        public App()
        {
            _mainForm = new MainForm(this);
            _isExiting = false;
        }
        public void Run()
        {
            Application.Run(_mainForm);
        }

        private void CloseAllForms()
        {
            _mainForm.Close();
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
