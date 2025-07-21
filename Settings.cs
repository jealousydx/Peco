using System.Diagnostics;
using System.Text.Json;

namespace Peco
{
    internal static class Settings
    {
        private readonly static string _appDataDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Peco");

        public static string SYSTEM_PROXY => "system_proxy";
        public static string TUN_MODE => "tun_mode";

        private static readonly Dictionary<string, string> DEFAULT_SETTINGS = new()
        {
            { "config_path", "config.json" },
            { "core_path", "sing-box.exe" },
            { "mode", "system_proxy" },
        };

        private static string _settingsFile = Path.Combine(_appDataDir, "settings.json");

        private static string _infoMessage = "";

        private static readonly string _defaultConfigPath = DEFAULT_SETTINGS["config_path"];
        private static readonly string _defaultCorePath = DEFAULT_SETTINGS["core_path"];
        private static readonly string _defaultMode = DEFAULT_SETTINGS["mode"];

        private static string _configPath = _defaultConfigPath;
        private static string _corePath = _defaultCorePath;
        private static string _mode = _defaultMode;

        public static string ConfigPath => _configPath;
        public static string CorePath => _corePath;
        public static string Mode => _mode;
        public static string Info => _infoMessage;

        public static void Init()
        {
            _infoMessage = Properties.Resources.info;

            if (!Directory.Exists(_appDataDir))
            {
                Directory.CreateDirectory(_appDataDir);
            }

            if (!File.Exists(_settingsFile))
            {
                WriteSettingsToFile(DEFAULT_SETTINGS);
                Apply(DEFAULT_SETTINGS);
            }
            else
            {
                var settings = ReadSettings();
                Apply(settings);
            }
        }

        private static void Apply(Dictionary<string, string> settings)
        {
            _configPath = settings.TryGetValue("config_path", out var cfg) ? cfg : _defaultConfigPath;
            _corePath = settings.TryGetValue("core_path", out var core) ? core : _defaultCorePath;
            _mode = settings.TryGetValue("mode", out var mode) ? mode : _defaultMode;
        }

        private static void WriteSettingsToFile(Dictionary<string, string> settings)
        {
            //
            var data = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsFile, data);
        }

        public static void OpenConfigFile()
        {
            if (!File.Exists(_configPath))
            {
                Alert.Error($"config file does not exist: {_configPath}");
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = _configPath,
                UseShellExecute = true
            });
        }

        public static void LoadConfigPath()
        {
            using OpenFileDialog openFileDialog = new();

            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.Title = "Choose a config file";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _configPath = openFileDialog.FileName;
            }
        }

        public static void Save()
        {
            var settings = new Dictionary<string, string>
            {
                ["config_path"] = _configPath,
                ["core_path"] = _corePath,
                ["mode"] = _mode
            };
            WriteSettingsToFile(settings);
        }

        private static Dictionary<string, string> ReadSettings()
        {
            try
            {
                var settings = File.ReadAllText(_settingsFile);
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(settings);
                return dict ?? new Dictionary<string, string>();
            }
            catch
            {
                return new Dictionary<string, string>();
            }
        }

        public static void SetMode(string mode)
        {
            _mode = mode;
        }
    }
}
