namespace Peco
{
    internal static class Config
    {
        private static bool _validated = true;
        public static bool Good => _validated;
        
        public static void Validate()
        {
            if (!File.Exists(Settings.ConfigPath))
            {
                // Settings.LoadDefaultConfig();

                _validated = false;
                return;
            }

            _validated = true;
        }
    }
}