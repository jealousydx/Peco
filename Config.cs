using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Peco
{
    internal static class Config
    {
        private static bool _validated = true;
        public static bool Good => _validated;
        
        public static void Build()
        {
            AddLogField();

            if (Settings.Mode == Settings.SYSTEM_PROXY)
            {
                AddSystemProxyInbound();
            }
            else
            {
                AddTunModeInbound();
            }
        }

        private static void AddLogField()
        {
            JsonObject config = JsonNode.Parse(File.ReadAllText(Settings.ConfigPath))!.AsObject();
            JsonNode newInboundsContent = JsonNode.Parse(Properties.Resources.log)!;

            config["log"] = newInboundsContent.DeepClone();
            File.WriteAllText(Settings.ConfigPath, config.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }

        private static void AddSystemProxyInbound()
        {
            JsonObject config = JsonNode.Parse(File.ReadAllText(Settings.ConfigPath))!.AsObject();
            JsonNode newInboundsContent = JsonNode.Parse(Properties.Resources.inbound_system_proxy)!;

            config["inbounds"] = newInboundsContent.DeepClone();
            File.WriteAllText(Settings.ConfigPath, config.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }

        private static void AddTunModeInbound()
        {
            JsonObject config = JsonNode.Parse(File.ReadAllText(Settings.ConfigPath))!.AsObject();
            JsonNode newInboundsContent = JsonNode.Parse(Properties.Resources.inbound_tun)!;

            config["inbounds"] = newInboundsContent.DeepClone(); 
            File.WriteAllText(Settings.ConfigPath, config.ToJsonString(new JsonSerializerOptions { WriteIndented = true }))
        }

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
            validator.Dispose();

            if (string.IsNullOrEmpty(error))
            {
                _validated = true;
                return;
            }

            Alert.Error(error);

            _validated = false;
        }
    }
}