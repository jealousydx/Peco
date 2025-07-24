namespace Peco
{
    internal partial class LogForm : Form
    {
        private readonly App _app;

        public LogForm(App context)
        {
            InitializeComponent();

            this.Hide();

            _app = context;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_app.IsExiting())
            {
                e.Cancel = true;
                this.Hide();
                return;
            }

            base.OnFormClosing(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Log.BindForm(this);
        }
    }
}
