namespace Peco
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

        public static void InvalidConfigFormat()
        {
            MessageBox.Show("Invalid config format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
