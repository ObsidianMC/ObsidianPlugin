using Obsidian.API;
using Obsidian.API.Data;
using Obsidian.API.Inventory.DataComponents;
using Obsidian.API.Plugins;
using Obsidian.Nbt;

namespace ObsidianPlugin;

/// <summary>
/// Counts trips to and from the dusk: each player's in an attachment saved with the player, and the whole world's in
/// saved data. Call <see cref="Register"/> from <see cref="PluginBase.ConfigureRegistry"/>, then
/// <see cref="Count"/> after each trip.
/// </summary>
/// <remarks>
/// Neither has a network codec: only the server reads them, so clients are sent nothing and vanilla clients can join.
/// </remarks>
public static class DuskVisits
{
    /// <summary>
    /// A player's trips, saved with the player as <c>obsidianplugin:dusk_visits</c>.
    /// </summary>
    public static AttachmentType<int> PlayerVisits { get; private set; } = null!;

    /// <summary>
    /// The world's trips, saved in <c>data/obsidian/world/obsidianplugin/dusk_visits.dat</c>.
    /// </summary>
    public static SavedDataType<WorldVisits> WorldVisits { get; private set; } = null!;

    /// <summary>
    /// Registers the attachment and the saved data type.
    /// </summary>
    public static void Register(IPluginRegistry registry)
    {
        // Kept through deaths (copyOnDeath), so that dying doesn't reset a player's count.
        PlayerVisits = registry.RegisterAttachment(
            "obsidianplugin:dusk_visits",
            new ComponentCodec<int>(visits => new NbtTag<int>("", visits), tag => ((NbtTag<int>)tag).Value),
            copyOnDeath: true);

        WorldVisits = registry.RegisterSavedData(
            "obsidianplugin:dusk_visits",
            new ComponentCodec<WorldVisits>(
                visits => new NbtTag<int>("", visits.Count),
                tag => new WorldVisits { Count = ((NbtTag<int>)tag).Value }));
    }

    /// <summary>
    /// Counts a trip by a player.
    /// </summary>
    /// <returns>The player's trips and the world's, this one included.</returns>
    public static (int Player, int World) Count(IServer server, IPlayer player)
    {
        // Attachment values are detached copies: read, change and set the value again.
        var playerVisits = player.GetAttachment(PlayerVisits) + 1;
        player.SetAttachment(PlayerVisits, playerVisits);

        // The world's count is one live object shared by every level and command, which can run concurrently, so
        // it's changed and marked dirty under its own lock.
        var saved = server.GetSavedData(WorldVisits, () => new WorldVisits());
        var world = saved.Value;

        lock (world.Lock)
        {
            world.Count++;
            saved.MarkDirty();

            return (playerVisits, world.Count);
        }
    }
}

/// <summary>
/// The world's trips to and from the dusk. Change <see cref="Count"/> only while holding <see cref="Lock"/>.
/// </summary>
public sealed class WorldVisits
{
    /// <summary>
    /// The lock guarding <see cref="Count"/> through its change and the save data's <c>MarkDirty</c>.
    /// </summary>
    public Lock Lock { get; } = new();

    public int Count { get; set; }
}
