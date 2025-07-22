namespace Peco
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            TurnOnButton = new Button();
            TurnOffButton = new Button();
            SettingsButton = new Button();
            ExitButton = new Button();
            Menu = new MenuStrip();
            ConfigButton = new ToolStripMenuItem();
            OpenConfigButton = new ToolStripMenuItem();
            LoadConfigButton = new ToolStripMenuItem();
            ModeButton = new ToolStripMenuItem();
            SetSystemProxyButton = new ToolStripMenuItem();
            SetTunModeButton = new ToolStripMenuItem();
            LogsButton = new ToolStripMenuItem();
            AboutPecoButton = new ToolStripMenuItem();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // TurnOnButton
            // 
            TurnOnButton.Location = new Point(10, 45);
            TurnOnButton.Name = "TurnOnButton";
            TurnOnButton.Size = new Size(210, 40);
            TurnOnButton.TabIndex = 0;
            TurnOnButton.Text = "On";
            TurnOnButton.UseVisualStyleBackColor = true;
            TurnOnButton.Click += TurnOnButton_Click;
            // 
            // TurnOffButton
            // 
            TurnOffButton.Enabled = false;
            TurnOffButton.Location = new Point(10, 100);
            TurnOffButton.Name = "TurnOffButton";
            TurnOffButton.Size = new Size(210, 40);
            TurnOffButton.TabIndex = 1;
            TurnOffButton.Text = "Off";
            TurnOffButton.UseVisualStyleBackColor = true;
            TurnOffButton.Click += TurnOffButton_Click;
            // 
            // SettingsButton
            // 
            SettingsButton.Enabled = false;
            SettingsButton.Location = new Point(10, 155);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(210, 40);
            SettingsButton.TabIndex = 2;
            SettingsButton.Text = "Settings";
            SettingsButton.UseVisualStyleBackColor = true;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(10, 210);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(210, 40);
            ExitButton.TabIndex = 3;
            ExitButton.Text = "Exit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // Menu
            // 
            Menu.Anchor = AnchorStyles.None;
            Menu.AutoSize = false;
            Menu.BackColor = SystemColors.ButtonFace;
            Menu.Dock = DockStyle.None;
            Menu.GripMargin = new Padding(0);
            Menu.ImageScalingSize = new Size(15, 15);
            Menu.Items.AddRange(new ToolStripItem[] { ConfigButton, ModeButton, LogsButton, AboutPecoButton });
            Menu.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            Menu.Location = new Point(0, 0);
            Menu.Name = "Menu";
            Menu.Padding = new Padding(15, 0, 0, 0);
            Menu.Size = new Size(235, 30);
            Menu.TabIndex = 4;
            Menu.Text = "menu";
            // 
            // ConfigButton
            // 
            ConfigButton.DropDownItems.AddRange(new ToolStripItem[] { OpenConfigButton, LoadConfigButton });
            ConfigButton.Name = "ConfigButton";
            ConfigButton.Size = new Size(55, 30);
            ConfigButton.Text = "Config";
            // 
            // OpenConfigButton
            // 
            OpenConfigButton.Name = "OpenConfigButton";
            OpenConfigButton.Size = new Size(100, 22);
            OpenConfigButton.Text = "Edit";
            OpenConfigButton.Click += OpenConfigButton_Click;
            // 
            // LoadConfigButton
            // 
            LoadConfigButton.Name = "LoadConfigButton";
            LoadConfigButton.Size = new Size(100, 22);
            LoadConfigButton.Text = "Load";
            LoadConfigButton.Click += LoadConfigButton_Click;
            // 
            // ModeButton
            // 
            ModeButton.DropDownItems.AddRange(new ToolStripItem[] { SetSystemProxyButton, SetTunModeButton });
            ModeButton.Name = "ModeButton";
            ModeButton.Size = new Size(50, 30);
            ModeButton.Text = "Mode";
            // 
            // SetSystemProxyButton
            // 
            SetSystemProxyButton.AutoSize = false;
            SetSystemProxyButton.CheckOnClick = true;
            SetSystemProxyButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SetSystemProxyButton.Name = "SetSystemProxyButton";
            SetSystemProxyButton.Size = new Size(180, 22);
            SetSystemProxyButton.Text = "System Proxy";
            SetSystemProxyButton.Click += SetSystemProxyButton_Click;
            // 
            // SetTunModeButton
            // 
            SetTunModeButton.CheckOnClick = true;
            SetTunModeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SetTunModeButton.Name = "SetTunModeButton";
            SetTunModeButton.Size = new Size(180, 22);
            SetTunModeButton.Text = "Tun Mode";
            SetTunModeButton.Click += SetTunModeButton_Click;
            // 
            // LogsButton
            // 
            LogsButton.Enabled = false;
            LogsButton.Name = "LogsButton";
            LogsButton.Size = new Size(44, 30);
            LogsButton.Text = "Logs";
            // 
            // AboutPecoButton
            // 
            AboutPecoButton.Name = "AboutPecoButton";
            AboutPecoButton.Size = new Size(52, 30);
            AboutPecoButton.Text = "About";
            AboutPecoButton.Click += AboutPecoButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(234, 266);
            Controls.Add(ExitButton);
            Controls.Add(SettingsButton);
            Controls.Add(TurnOffButton);
            Controls.Add(TurnOnButton);
            Controls.Add(Menu);
            Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = Menu;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Peco";
            Menu.ResumeLayout(false);
            Menu.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button TurnOnButton;
        private Button TurnOffButton;
        private Button SettingsButton;
        private Button ExitButton;
        private MenuStrip Menu;
        private ToolStripMenuItem ModeButton;
        private ToolStripMenuItem ConfigButton;
        private ToolStripMenuItem LogsButton;
        private ToolStripMenuItem OpenConfigButton;
        private ToolStripMenuItem LoadConfigButton;
        private ToolStripMenuItem SetSystemProxyButton;
        private ToolStripMenuItem SetTunModeButton;
        private ToolStripMenuItem AboutPecoButton;
    }
}
