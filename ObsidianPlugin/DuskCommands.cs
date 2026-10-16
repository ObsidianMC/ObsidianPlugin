using Microsoft.Extensions.Logging;
using Obsidian.API;
using Obsidian.API.Commands;
using Obsidian.API.Plugins;
using ObsidianPlugin.Portal;

namespace ObsidianPlugin;

/// <summary>
/// The plugin's commands. <c>registry.MapCommands()</c> finds this class, like any <see cref="CommandModuleBase"/>
/// in the plugin, and the server creates one for each command it runs.
/// </summary>
public sealed class DuskCommands : CommandModuleBase
{
    // Services are injected into properties marked [Inject].
    [Inject]
    public ILogger<DuskCommands> Logger { get; set; } = null!;

    /// <summary>
    /// Sends the player to the dusk's spawn, or from the dusk back to the overworld, and counts the trip.
    /// </summary>
    [Command("dusk")]
    [CommandInfo("Takes you to the dusk, or back to the overworld.", "/dusk")]
    [IssuerScope(CommandIssuers.Client)]
    public async Task DuskAsync()
    {
        if (this.Player is not IPlayer player)
            return;

        // The overworld is the root level of the player's world, which the dusk belongs to.
        var destination = player.Level is IDimension { DimensionName: DuskPortal.Dimension } dusk
            ? dusk.ParentWorld.DimensionName
            : DuskPortal.Dimension;

        // False when the world has no such dimension, or the player is dead.
        if (!await player.TeleportToDimensionAsync(destination))
        {
            await player.SendMessageAsync("You can't travel right now.");
            return;
        }

        var (playerVisits, worldVisits) = DuskVisits.Count(this.Server, player);
        this.Logger.LogDebug("{Player} travelled to {Destination}", player.Username, destination);

        await player.SendMessageAsync($"Dusk trips: {playerVisits} by you, {worldVisits} by everyone.");
    }
}
