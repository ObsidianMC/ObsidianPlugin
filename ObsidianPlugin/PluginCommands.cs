using Microsoft.Extensions.Logging;
using Obsidian.API;
using Obsidian.API.Commands;
using Obsidian.API.Plugins;
using System.Threading.Tasks;

namespace ObsidianPlugin;

//All command modules are created with a scoped lifetime
public class MyCommands : CommandModuleBase
{
    [Inject]
    public ILogger<MyCommands> Logger { get; set; }

    [Command("mycommand")]
    [CommandInfo("woop dee doo this command is from a plugin")]
    public async Task MyCommandAsync()
    {
        Logger.LogInformation("Testing Services as injected dependency");
        await this.Player.SendMessageAsync("Hello from plugin command!");
    }
}
