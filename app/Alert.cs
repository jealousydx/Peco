namespace Peco.app
{
    internal static class Alert
    {
        public static void Warning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Information(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void InvalidModeException()
        {
            MessageBox.Show($"MODE INITED: {Settings.Mode}", "InvalidModeException", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void TunModeError()
        {
            MessageBox.Show("** Restart Peco.exe as Admin **", "Tun Mode", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
