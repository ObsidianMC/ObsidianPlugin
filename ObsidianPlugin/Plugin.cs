/**
 * 
 * It is STRONGLY recommended to get familiar with both the API documentation and the basic plugin guide.
 * These can be found here:
 * https://obsidian-mc.net/articles/plugins.html
 * https://obsidian-mc.net/api/index.html
 * 
 * Please DO take note that Obsidian is still in active development,
 * meaning both the server and it's API are extremely prone to changes!
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using Obsidian.API;
using Obsidian.API.Events;
using Obsidian.API.Plugins;

namespace ObsidianPlugin
{
    public class Plugin : PluginBase
    {
        // Any interface from Obsidian.Plugins.Services can be injected into properties
        [Inject] public ILogger<Plugin> Logger { get; set; }

        public override void ConfigureRegistry(IPluginRegistry pluginRegistry)
        {
            pluginRegistry.MapEvent(OnPlayerJoin);
            pluginRegistry.MapCommand("plugincommand", PluginCommandAsync);
        }

        // One of server messages, called when an event occurs
        public override ValueTask OnLoadAsync(IServer server)
        {
            Logger.LogInformation("§a{name} §floaded! §aHello World§f!", Info.Name);
            Logger.LogInformation("Hello! I live at §a{location}§f!", this.GetType().Assembly.Location);
            return ValueTask.CompletedTask;
        }

        public async Task OnPlayerJoin(PlayerJoinEventArgs playerJoinEvent)
        {
            var player = playerJoinEvent.Player;
            await player.SendMessageAsync(message: ChatMessage.Simple(text: $"Welcome {player.Username}!", color: ChatColor.Gold));
        }

        [CommandInfo(description: "Command defined in the plugin's own class.")]
        public async Task PluginCommandAsync(CommandContext ctx)
        {
            await ctx.Player.SendMessageAsync(
                new ChatMessage()
                .AppendColor(ChatColor.BrightGreen)
                .AppendText("Hello plugin world!"));
        }
    }
}