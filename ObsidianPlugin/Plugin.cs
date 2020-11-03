using Obsidian.API;
using Obsidian.API.Plugins;
using Obsidian.API.Plugins.Services;
using Obsidian.CommandFramework.Attributes;
using Obsidian.CommandFramework.Entities;
using System.Threading.Tasks;

namespace ObsidianPlugin
{
    [Plugin(Name = "ObsidianPlugin", Version = "1.0",
            Authors = "Naamloos", Description = "A test plugin with nuget.",
            ProjectUrl = "https://github.com/Naamloos/ObsidianPlugin")]
    public class Plugin : PluginBase
    {
        // Any interface from Obsidian.Plugins.Services can be injected into properties
        [Inject] public ILogger Logger { get; set; }

        // One of server messages, called when an event occurs
        public async Task OnLoad(IServer server)
        {
            Logger.Log($"Loaded {Info.Name}.");
            server.RegisterCommandClass<PluginCommands>();
            await Task.CompletedTask;
        }

        public class PluginCommands : BaseCommandClass
        {
            [Command("health")]
            [CommandInfo("Reads your health", "/health")]
            public async Task HealthAsync(ObsidianContext ctx)
            {
                await ctx.Player.SendMessageAsync(ctx.Player.Health.ToString());
            }
        }
    }
}
