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
    public class PluginCommands // Fun fact: You can actually move this class to a different file and it'll work just fine.
    {
        [Inject]
        public Plugin Plugin { get; set; }

        [Inject]
        public ILogger Logger { get; set; }

        [Command("classcommand")]
        [CommandInfo("woop dee doo this command is from a plugin's command class!!")]
        public async Task MyCommandAsync(CommandContext ctx)
        {
            Plugin.Logger.Log("Testing Plugin as injected dependency");
            Logger.Log("Testing Services as injected dependency");
            await ctx.Player.SendMessageAsync("Hello from plugin command!");
        }
    }
}
