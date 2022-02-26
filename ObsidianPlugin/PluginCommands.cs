using Obsidian.API;
using Obsidian.API.Plugins;
using Obsidian.API.Plugins.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsidianPlugin
{
    [CommandRoot]
    public class PluginCommands
    {
        [Inject]
        public Plugin Plugin { get; set; }

        [Inject]
        public ILogger Logger { get; set; }

        [Command("classcommand")]
        [CommandInfo("Command defined in the plugin's own class.")]
        public async Task MyCommandAsync(CommandContext ctx)
        {
            Plugin.Logger.Log("Hello plugin dependency world!");
            Logger.Log("Hello logger dependency world!");
            await ctx.Player.SendMessageAsync(
                new ChatMessage()
                .AppendColor(ChatColor.BrightGreen)
                .AppendText("Hello command class world!"));
        }
    }
}
