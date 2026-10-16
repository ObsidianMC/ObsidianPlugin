# ObsidianPlugin

A template repository and sample for [Obsidian](https://github.com/ObsidianMC/Obsidian) server plugins. It adds a
dimension, a portal to it and a command that counts the trips there, using the public API alone (`Obsidian.API`, whose
package includes `Obsidian.Nbt` for saved values).

## Server-side only

The plugin's side is `server` (`PluginSide` in the project): it runs on the server only, and the Obsidian client
skips it. Vanilla clients can join a server running it, because it adds only content they can join with. Once any
plugin registers blocks, items, entity types, block entity types, menu types, recipe serializers or data component
types, the server refuses vanilla clients, since those registries must match the client's. So this sample registers
none of them:

- **The dusk**, `obsidianplugin:dusk`: a dimension in data alone. `data/obsidianplugin/dimension/dusk.json` is a
  flat level of end stone over bedrock, and its type, `data/obsidianplugin/dimension_type/dusk.json`, gives it the
  End's sky and a violet fog. Clients are sent dimension types with their data, so vanilla clients draw it too.
- **The dusk portal** ([Portal/DuskPortal.cs](ObsidianPlugin/Portal/DuskPortal.cs)): a frame of amethyst blocks, at
  least 2 by 3 inside, lit by using an amethyst shard on it, fills with vanilla's Nether portal block and takes what
  stands in it to the same place in the dusk, or back to the overworld, building a frame there if none is near. The
  server claims each portal block it lights for the dusk portal, so it never leads to the Nether.
- **`/dusk`** ([DuskCommands.cs](ObsidianPlugin/DuskCommands.cs)): sends the player to the dusk, or back to the
  overworld from there, and replies with how many trips they and the whole world have made. Each player's count is
  an attachment, `obsidianplugin:dusk_visits`, saved with the player; the world's is saved data of the same name
  ([DuskVisits.cs](ObsidianPlugin/DuskVisits.cs)). Neither is sent to clients.
- **A greeting** ([JoinGreeter.cs](ObsidianPlugin/JoinGreeter.cs)): an event handler telling joining players about
  the portal and `/dusk`.

[ObsidianPlugin.cs](ObsidianPlugin/ObsidianPlugin.cs) registers them all.

For a mod with its own blocks, items or entities, and a client side (screens, renderers, key bindings), start from
[ObsidianMod](https://github.com/ObsidianMC/ObsidianMod) instead. Players then need the mod on their client.

## Building

```
dotnet build ObsidianPlugin/ObsidianPlugin.slnx
```

[Obsidian.MSBuild](https://www.nuget.org/packages/Obsidian.MSBuild) packs the build into
`ObsidianPlugin/bin/<Configuration>/ObsidianPlugin.obby`: the plugin's assembly, its `data/` folder and
`obsidian.mod.json`, generated from the metadata properties in `ObsidianPlugin.csproj`. Obsidian.API and Obsidian.Nbt
aren't packed: the server provides them.

## Installing

Copy `ObsidianPlugin.obby` into the server's `plugins` folder and start the server, which creates the dusk as it
loads the plugin.

## Making it yours

1. Rename the project, solution and folder (`ObsidianPlugin`), and the root namespace in the `.cs` files.
2. In `ObsidianPlugin.csproj`, set `PluginId` (your namespace, lowercase), `Version`, `Title`, `Authors`,
   `Description` and `ProjectUrl`.
3. Replace the `obsidianplugin` namespace everywhere it's used: the identifiers in the `.cs` files and the
   `data/obsidianplugin/` folder, including the `"type"` in `dimension/dusk.json`.
4. Remove the dusk, its portal and `/dusk` once you don't need them as examples.

## Documentation

[plugin-content.md](https://github.com/ObsidianMC/Obsidian/blob/1.21.x/docs/plugin-content.md) describes the API:
commands, events, dimensions, portals, saved data and attachments, and the content that needs a modded client.

Obsidian is still in active development, so both the server and its API are prone to change.
