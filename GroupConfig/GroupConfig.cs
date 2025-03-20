using TerrariaApi.Server;
using TShockAPI;
using Terraria;
using TShockAPI.Hooks;

namespace GroupConfig
{
    [ApiVersion(2, 1)]
    public class GroupConfigPlugin : TerrariaPlugin
    {
        public override string Author => "Keyou";
        public override string Description => "Easily configure groups.";
        public override string Name => "GroupConfig";
        public override Version Version => new Version(1, 0, 2);

        private static GroupConfig.Config? config;

        public GroupConfigPlugin(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            config = GroupConfig.Config.Read();
            GeneralHooks.ReloadEvent += OnReload;
            ServerApi.Hooks.GamePostInitialize.Register(this, PostInitialize);
        }

        public void PostInitialize(EventArgs e)
        {
            config?.RewriteConfig();
            LoadGroup();
            TShock.Log.ConsoleInfo("[Group Config] has been loaded.");
        }

        public void OnReload(ReloadEventArgs e)
        {
            config = GroupConfig.Config.Read();
            config.RewriteConfig();
            LoadGroup();
            TShock.Log.ConsoleInfo("[Group Config] has been reloaded.");
        }

        public static void LoadGroup()
        {
            int counter = 1;
            if (config == null || config.Groups == null)
            {
                TShock.Log.ConsoleError("[Group Config] Config for Groups is null.");
                return;
            }

            foreach (var key in config.Groups)
            {
                string name = key.Key;
                string parent = key.Value.Parent ?? "";
                string permissions = key.Value.Command ?? "";
                int[] color = key.Value.ChatColor;
                string chatColor = $"{color[0]},{color[1]},{color[2]}";
                string prefix = key.Value.Prefix ?? "";
                string suffix = key.Value.Suffix ?? "";

                counter++;
                var group = TShock.Groups.GetGroupByName(name);
                if (group == null)
                {
                    TShock.Log.ConsoleWarn("[Group Config] Group not found: " + name);
                }
                else
                {
                    if (name == "superadmin")
                    {
                        group.Prefix = prefix;
                        group.Suffix = suffix;
                        group.ChatColor = chatColor;
                    }
                    else
                    {
                        TShock.Groups.UpdateGroup(name, parent, permissions, chatColor, suffix, prefix);
                    }
                }
            }
        }
    }
}
