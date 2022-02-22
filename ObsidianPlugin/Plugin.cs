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

using Obsidian.API;
using Obsidian.API.Events;
using Obsidian.API.Plugins;
using Obsidian.API.Plugins.Services;
using System;
using System.Threading.Tasks;

namespace ObsidianPlugin
{
    [Plugin(name: "Template Plugin", Version = "1.0",
            Authors = "Obsidian Team", Description = "My Plugin.",
            ProjectUrl = "https://github.com/ObsidianMC/Obsidian")]
    public class ObsidianPlugin : PluginBase
    {
        // Any interface from Obsidian.Plugins.Services can be injected into properties
        [Inject] public ILogger Logger { get; set; }
        [Inject] public IFileReader FileReader { get; set; }

        // One of server messages, called when an event occurs
        public void OnLoad(IServer server)
        {
            Logger.Log(message: $"§a{Info.Name} §floaded! Hello §aWorld§f!");
            Logger.Log(message: $"Hello! I live at §a{FileReader.CreateWorkingDirectory()}§f!");
        }

        public async Task OnPlayerJoin(PlayerJoinEventArgs playerJoinEvent)
        {
            var player = playerJoinEvent.Player;

            await player.SendMessageAsync(message: ChatMessage.Simple(text: $"Welcome {player.Username}!", color: ChatColor.Gold));
        }

        [Command(commandName: "plugincommand")]
        [CommandInfo(description: "woop dee doo this command is from within a plugin's own class!!")]
        public async Task PluginCommandAsync(CommandContext ctx)
        {
            await ctx.Sender.SendMessageAsync(message: "Hello from plugin command implemented in Plugin class!");
        }
    }

    [CommandRoot]
    public class MyCommands // Fun fact: You can actually move this class to a different file and it'll work just fine.
    {
        [Inject]
        public ObsidianPlugin Plugin { get; set; }

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