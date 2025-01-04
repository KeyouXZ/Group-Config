using Newtonsoft.Json;
using System.Text.RegularExpressions;
using TShockAPI;

namespace GroupConfig
{
    public class Config
    {
        [JsonProperty("Blacklist")]
        public string[] Blacklist { get; set; } = new string[] { };
        public Dictionary<string, Group> Groups { get; set; } = new Dictionary<string, Group>();
        public static Config Read()
        {
            try
            {
                string configPath = Path.Combine(TShock.SavePath, "GroupConfig.json");
                Config config = new Config().defaultConfig();

                if (!File.Exists(configPath))
                {
                    File.WriteAllText(configPath, JsonConvert.SerializeObject(config, Formatting.Indented));
                }
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath)) ?? new Config();

                return config;
            }

            catch (Exception ex)
            {
                TShock.Log.ConsoleError(ex.ToString());
                return new Config();
            }
        }

        private bool IsBlacklisted(string groupName)
        {
            foreach (var pattern in Blacklist)
            {
                string regexPattern = "^" + Regex.Escape(pattern).Replace("\\*", ".*") + "$";
                if (Regex.IsMatch(groupName, regexPattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public void RewriteConfig()
        {
            string configPath = Path.Combine(TShock.SavePath, "GroupConfig.json");

            foreach (var pattern in Blacklist)
            {
                string regexPattern = "^" + Regex.Escape(pattern).Replace("\\*", ".*") + "$";
                Groups = Groups.Where(kvp => !Regex.IsMatch(kvp.Key, regexPattern, RegexOptions.IgnoreCase))
                               .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }

            File.WriteAllText(configPath, JsonConvert.SerializeObject(this, Formatting.Indented));
            TShock.Log.ConsoleInfo("[Group Config] Config file has been rewritten.");
        }

        private Config defaultConfig()
        {
            var defaultConfig = new Config();
            foreach (var tshockGroup in TShock.Groups.groups)
            {

                if (IsBlacklisted(tshockGroup.Name))
                {
                    continue;
                }

                var group = new Group
                {
                    Parent = tshockGroup.ParentName,
                    Command = tshockGroup.Permissions,
                    ChatColor = new int[] { tshockGroup.R, tshockGroup.G, tshockGroup.B },
                    Prefix = tshockGroup.Prefix,
                    Suffix = tshockGroup.Suffix
                };
                defaultConfig.Groups.Add(tshockGroup.Name, group);
            }
            return defaultConfig;
        }

        public class Group
        {
            [JsonProperty("Parent")]
            public string Parent { get; set; } = string.Empty;

            [JsonProperty("Commands")]
            public string Command { get; set; } = string.Empty;

            [JsonProperty("Chat Color")]
            public int[] ChatColor { get; set; } = new int[] { };

            [JsonProperty("Prefix")]
            public string Prefix { get; set; } = string.Empty;

            [JsonProperty("Suffix")]
            public string Suffix { get; set; } = string.Empty;
        }
    }
}
