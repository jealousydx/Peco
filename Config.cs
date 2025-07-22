namespace Peco
{
    internal static class Config
    {
        private static bool _validated = true;
        public static bool Good => _validated;
        
        public static void Validate()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = Settings.CorePath,
                Arguments = $"check -c {Settings.ConfigPath}",
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            var validator = new Process { StartInfo = startInfo };
            
            validator.Start();

            string error = validator.StandardError.ReadToEnd();

            validator.WaitForExit();

            if (string.IsNullOrEmpty(error))
            {
                _validated = true;
                return;
            }

            _validated = false;
        }
    }
}