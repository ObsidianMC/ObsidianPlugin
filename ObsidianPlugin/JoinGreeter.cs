using Obsidian.API;
using Obsidian.API.Events;

namespace ObsidianPlugin;

/// <summary>
/// The plugin's event handlers. <c>registry.MapEvents()</c> finds this class, like any
/// <see cref="MinecraftEventHandler"/> in the plugin; each public method marked <see cref="EventPriorityAttribute"/>
/// handles the event its parameter is the arguments of. The server creates the class for each event it handles.
/// </summary>
public sealed class JoinGreeter : MinecraftEventHandler
{
    /// <summary>
    /// Greets a joining player and tells them how to reach the dusk.
    /// </summary>
    [EventPriority(Priority = Priority.Normal)]
    public async ValueTask OnPlayerJoinAsync(PlayerJoinEventArgs args)
    {
        await args.Player.SendMessageAsync($"Welcome, {args.Player.Username}!");
        await args.Player.SendMessageAsync(
            "Light a frame of amethyst blocks with an amethyst shard to open a portal to the dusk, or type /dusk.");
    }
}
