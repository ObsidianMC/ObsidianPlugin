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
            [Command("click")]
            [CommandInfo("Posts a clickable link to chat", "/health")]
            public async Task HealthAsync(ObsidianContext ctx, [Remaining]string link)
            {
                var chat = IChatMessage.CreateNew();
                chat.Text = $"<{ctx.Player.Username}>: {link}";
                chat.ClickEvent.Action = ETextAction.OpenUrl;
                chat.ClickEvent.Value = link;
                chat.HoverEvent.Action = ETextAction.ShowText;
                chat.HoverEvent.Value = "Click to open link";
                chat.Underline = true;

                await ctx.Player.SendMessageAsync(ctx.Player.Health.ToString());
            }
        }
    }
}
