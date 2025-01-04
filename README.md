# GroupConfig Plugin

GroupConfig is a plugin for TShock that allows you to manage and configure groups in Terraria. This plugin provides an easy way to handle group permissions, chat colors, prefixes, and suffixes.

## Features

- Load and configure groups from a JSON file.
- Set chat colors, prefixes, and suffixes for groups.
- Manage group permissions.
- Blacklist group.

## Installation

1. Download the latest release of the plugin.
2. Place the `GroupConfig.dll` file into the `ServerPlugins` folder of your TShock installation.
3. Restart your TShock server.

## Configuration

The configuration file `GroupConfig.json` will be generated in the TShock save path upon the first run of the plugin. You can edit this file to configure your groups.

### Example Configuration

```json
{
  "Blacklist": [
    "rank_*",
    "superadmin"
  ],
  "Groups": {
    "admin": {
      "Parent": "newadmin",
      "Commands": "tshock.admin.ban,tshock.cfg.whitelist,tshock.npc.spawnboss,tshock.npc.spawnmob,tshock.admin.warp,tshock.world.time.set,tshock.tp.self,tshock.slap,tshock.kill,tshock.admin.viewlogs,tshock.admin.nokick,tshock.tp.others,tshock.accountinfo.details,tshock.admin.broadcast,tshock.tp.home,tshock.tp.allothers,tshock.tp.block,tshock.tp.npc,tshock.tp.pos,tshock.tp.silent,tshock.admin.userinfo,tshock.tp.spawn",
      "Chat Color": [
        255,
        255,
        255
      ],
      "Prefix": null,
      "Suffix": null
    },
    "trustedadmin": {
      "Parent": "admin",
      "Commands": "tshock.cfg.maintenance,tshock.cfg.*,tshock.world.*,tshock.npc.butcher,tshock.item.spawn,tshock.item.give,tshock.heal,tshock.admin.noban,tshock.item.usebanned,tshock.ignore.sendtilesquare,tshock.buff.self,tshock.buff.others,tshock.clear,tshock.npc.clearanglerquests,tshock.godmode,tshock.godmode.other,tshock.ignore.damage,tshock.ignore.hp,tshock.ignore.removetile,tshock.ignore.liquid,tshock.ignore.mp,tshock.ignore.paint,tshock.ignore.placetile,tshock.ignore.projectile,tshock.ignore.itemstack,tshock.npc.invade,tshock.npc.startdd2,tshock.ssc.upload,tshock.ssc.upload.others,tshock.npc.spawnpets,tshock.journey.time.freeze,tshock.journey.time.set,tshock.journey.time.setspeed,tshock.journey.godmode,tshock.journey.wind.strength,tshock.journey.wind.freeze,tshock.journey.rain.strength,tshock.journey.rain.freeze,tshock.journey.placementrange,tshock.journey.setdifficulty,tshock.journey.biomespreadfreeze,tshock.journey.setspawnrate,tshock.journey.research",
      "Chat Color": [
        255,
        255,
        255
      ],
      "Prefix": null,
      "Suffix": null
    },
  }
}
```

## Usage

The plugin will automatically load the groups from the configuration file when the server starts. You can also reload the configuration by using the `/reload` command in TShock.

## Contributing

If you would like to contribute to the development of this plugin, feel free to fork the repository and submit a pull request.

## License

This project is licensed under the [MIT License](./LICENSE).