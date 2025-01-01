using Newtonsoft.Json;
using TShockAPI;

namespace GroupConfig {
    public class Config
    {
        
        public Dictionary<string, Group> Groups { get; set; } = new Dictionary<string, Group>();
        public static Config Read() {
            string configPath = Path.Combine(TShock.SavePath, "GroupConfig.json");

            try {
				Config config = new Config().defaultConfig();

				if (!File.Exists(configPath)) {
					File.WriteAllText(configPath, JsonConvert.SerializeObject(config, Formatting.Indented));
				}
				config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath)) ?? new Config();

				return config;
			}
			
			catch (Exception ex) {
				TShock.Log.ConsoleError(ex.ToString());
				return new Config();
			}
        }

        private Config defaultConfig()
        {
            var defaultConfig = new Config();
            foreach (var tshockGroup in TShock.Groups.groups)
            {
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
            public int[] ChatColor { get; set; } = new int[]{};

            [JsonProperty("Prefix")]
            public string Prefix { get; set; } = string.Empty;

            [JsonProperty("Suffix")]
            public string Suffix { get; set; } = string.Empty;
        }
    }
}