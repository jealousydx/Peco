using Peco.app;

namespace Peco
{
    internal partial class MainForm : Form
    {
        private NotifyIcon _trayIcon = new();
        private ContextMenuStrip _trayMenu = new();

        private ToolStripMenuItem _onMenuItem = new();
        private ToolStripMenuItem _offMenuItem = new();
        private ToolStripMenuItem _exitMenuItem = new();

        private const int WM_ENDSESSION = 0x0016;

        public MainForm()
        {
            InitializeComponent();
            InitializeTrayIcon();
            InitializeMode();
            InitializeWindowText();
        }

        private void InitializeMode()
        {
            if (Settings.Mode == Settings.TUN_MODE)
            {
                SetButtonsStateTunMode();
            }
            else if (Settings.Mode == Settings.SYSTEM_PROXY)
            {
                SetButtonsStateSystemProxyMode();
            }
            else
        private void InitializeWindowText()
        {
            if (Program.IsElevated)
            {
                this.Text += " (admin)";
            }
        }

        private void InitializeTrayIcon()
        {
            _trayMenu = new ContextMenuStrip();

            _onMenuItem = new ToolStripMenuItem("On", null, TurnOnButton_Click);
            _offMenuItem = new ToolStripMenuItem("Off", null, TurnOffButton_Click);
            _exitMenuItem = new ToolStripMenuItem("Exit", null, ExitButton_Click);

            _trayMenu.Items.Add(_onMenuItem);
            _trayMenu.Items.Add(_offMenuItem);
            _trayMenu.Items.Add(_exitMenuItem);

            _trayIcon.Text = "Peco";
            _trayIcon.Icon = this.Icon;
            _trayIcon.ContextMenuStrip = _trayMenu;
            _trayIcon.Visible = true;

            _trayIcon.MouseClick += TrayIcon_MouseClick;

            SetButtonsStateOff();
        }

        private void TrayIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                WindowState = FormWindowState.Normal;
                Activate();
            }
        }

        private void TurnOnButton_Click(object? sender, EventArgs e)
        {
            Log.Clear();
            Core.TurnOn();

            if (Core.SUCCESS)
            {
                SetButtonsStateOn();
            }
        }

        private void TurnOffButton_Click(object? sender, EventArgs e)
        {
            Core.TurnOff();

            SetButtonsStateOff();
        }

        private void LoadConfigButton_Click(object sender, EventArgs e)
        {
            Core.TurnOff();
            Settings.LoadConfigPath();

            SetButtonsStateOff();
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            //
        }

        private void OpenConfigButton_Click(object sender, EventArgs e)
        {
            Settings.OpenConfigFile();
        }

        private void ExitButton_Click(object? sender, EventArgs e)
        {
            this.DisposeTrayIcon();

            App.Exit();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ENDSESSION)
            {
                if (!Core.Enabled)
                {
                    base.WndProc(ref m);
                }

                if (Settings.Mode == Settings.TUN_MODE)
                {
                    SingTunAdapter.Remove();
                }
                else
                {
                    Proxy.Disable();
                }
            }

            base.WndProc(ref m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!App.Exiting)
            {
                e.Cancel = true;
                this.Hide();
                return;
            }

            this.DisposeTrayIcon();

            base.OnFormClosing(e);
        }

        private void AboutPecoButton_Click(object sender, EventArgs e)
        {
            Alert.Information(Settings.Info);
        }

        private void LogsButton_Click(object sender, EventArgs e)
        {
            if (!Core.Enabled)
            {
                Alert.Error("The core is disabled");
                return;
            }

            App.ShowLogForm();
        }

        private void DisposeTrayIcon()
        {
            if (_trayIcon != null)
            {
                _trayIcon.Visible = false;
                _trayIcon.Dispose();
            }
        }

        private void SetSystemProxyButton_Click(object sender, EventArgs e)
        {
            Settings.SetMode(Settings.SYSTEM_PROXY);
            Core.TurnOff();

            SetButtonsStateOff();
            SetButtonsStateSystemProxyMode();
        }

        private void SetTunModeButton_Click(object sender, EventArgs e)
        {
            Settings.SetMode(Settings.TUN_MODE);
            Core.TurnOff();

            SetButtonsStateOff();
            SetButtonsStateTunMode();
        }

        private void SetButtonsStateTunMode()
        {
            SetTunModeButton.Checked = true;
            SetSystemProxyButton.Checked = false;
        }

        private void SetButtonsStateSystemProxyMode()
        {
            SetTunModeButton.Checked = false;
            SetSystemProxyButton.Checked = true;
        }

        private void SetButtonsStateOn()
        {
            TurnOnButton.Enabled = false;
            TurnOffButton.Enabled = true;

            _onMenuItem.Enabled = false;
            _offMenuItem.Enabled = true;

            TurnOffButton.Focus();
        }

        private void SetButtonsStateOff()
        {
            TurnOnButton.Enabled = true;
            TurnOffButton.Enabled = false;

            _onMenuItem.Enabled = true;
            _offMenuItem.Enabled = false;

            TurnOnButton.Focus();
        }
    }
}