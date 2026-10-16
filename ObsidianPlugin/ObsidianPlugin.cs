/**
 *
 * Welcome to the Obsidian plugin sample! It's a server-side plugin: it runs on the server only, and adds nothing
 * that vanilla clients need a mod for, so they can still join. See README.md for what it does.
 *
 * It is STRONGLY recommended to get familiar with the plugin documentation:
 * https://github.com/ObsidianMC/Obsidian/blob/1.21.x/docs/plugin-content.md
 *
 * Please DO take note that Obsidian is still in active development,
 * meaning both the server and its API are prone to changes!
 *
 */

using Microsoft.Extensions.Logging;
using Obsidian.API;
using Obsidian.API.Plugins;
using ObsidianPlugin.Portal;

namespace ObsidianPlugin;

public sealed class ObsidianPlugin : PluginBase
{
    // Services are injected into properties marked [Inject], such as the server's logger.
    [Inject]
    public ILogger<ObsidianPlugin> Logger { get; set; } = null!;

    // Register the plugin's content here: commands, events, portals, saved data and attachments, and also blocks,
    // items and entities. Registering blocks, items, entities or other synced content makes the server refuse
    // vanilla clients, so this sample registers none: its portal uses vanilla's portal block, and its dimension is
    // data (data/obsidianplugin/), which the server loads by itself.
    public override void ConfigureRegistry(IPluginRegistry registry)
    {
        // Scans the plugin for command classes (CommandModuleBase) and registers their commands: DuskCommands.
        registry.MapCommands();

        // Scans the plugin for classes that inherit from MinecraftEventHandler: JoinGreeter.
        registry.MapEvents();

        DuskPortal.Register(registry);
        DuskVisits.Register(registry);
    }

    // Called when the plugin has fully loaded.
    public override ValueTask OnLoadedAsync(IServer server)
    {
        this.Logger.LogInformation(
            "§a{PluginName} §floaded! Light an amethyst portal to visit the dusk.",
            this.Info.Name);

        return ValueTask.CompletedTask;
    }

    // Called when the plugin is being unloaded.
    public override ValueTask OnUnloadingAsync()
    {
        this.Logger.LogInformation("{PluginName} is unloading.", this.Info.Name);

        return ValueTask.CompletedTask;
    }
}
