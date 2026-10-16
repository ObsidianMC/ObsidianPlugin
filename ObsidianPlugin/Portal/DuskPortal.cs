using Obsidian.API;
using Obsidian.API.Blocks;
using Obsidian.API.Plugins;
using Obsidian.API.Registries;
using Obsidian.API.World;
using Obsidian.API.World.Generator;

namespace ObsidianPlugin.Portal;

/// <summary>
/// A portal to the plugin's dimension, <c>obsidianplugin:dusk</c>: a frame of amethyst blocks, at least 2 by 3
/// inside, lit by using an amethyst shard on it, fills with portal blocks that send what stands in them to the same
/// place in the other level, building a frame there if none is near.
/// </summary>
/// <remarks>
/// <para>
/// The dimension is data, like a data pack's: <c>data/obsidianplugin/dimension/dusk.json</c> is a flat level of end
/// stone whose type, <c>data/obsidianplugin/dimension_type/dusk.json</c>, gives it the End's sky and a violet fog.
/// Vanilla clients are sent dimension types with their data, so they draw it without a mod. The server creates its
/// level as the plugin loads.
/// </para>
/// <para>
/// The portal fills with vanilla's Nether portal block rather than a block of its own: registering a block would make
/// the server refuse vanilla clients. The server claims each portal block it lights for this portal type, so these
/// portals never lead to the Nether, and Nether portals never lead to the dusk.
/// </para>
/// </remarks>
public static class DuskPortal
{
    /// <summary>
    /// The dimension's identifier, which is its level's name.
    /// </summary>
    public const string Dimension = "obsidianplugin:dusk";

    /// <summary>
    /// The portal type, set by <see cref="Register"/>.
    /// </summary>
    public static PortalType Type { get; private set; } = null!;

    /// <summary>
    /// Registers the portal type. Call from <see cref="PluginBase.ConfigureRegistry"/>.
    /// </summary>
    public static void Register(IPluginRegistry registry)
    {
        Type = registry.RegisterPortal("obsidianplugin:dusk", new PortalProperties
        {
            IsFrame = block => block.Is(BlockIds.AmethystBlock),
            FrameBlock = new SimpleBlockState { Name = "minecraft:amethyst_block" }.ToBlock(),
            PortalBlock = new SimpleBlockState { Name = "minecraft:nether_portal" }.ToBlock(),
            Igniter = ItemsRegistry.AmethystShard,
            ResolveTarget = Target
        });
    }

    // From the dimension back to the overworld, its world's root level, and from that to the dimension. Other
    // levels have no dusk portals: they don't light there.
    private static ILevel? Target(ILevel level)
    {
        if (level is IDimension { DimensionName: Dimension } dusk)
            return dusk.ParentWorld;

        return level is IWorld world ? world.GetLevel(Dimension) : null;
    }
}
