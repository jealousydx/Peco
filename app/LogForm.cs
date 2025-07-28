using Peco.app;

namespace Peco
{
    internal partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();

            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!App.Exiting)
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
