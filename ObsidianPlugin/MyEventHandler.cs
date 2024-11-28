using Obsidian.API;
using Obsidian.API.Events;
using System.Threading.Tasks;

namespace ObsidianPlugin;

//All event handlers are created with a scoped lifetime
public class MyEventHandler : MinecraftEventHandler
{
    [EventPriority(Priority = Priority.Critical)]
    public async ValueTask ChatEvent(IncomingChatMessageEventArgs args)
    {
        await args.Player.SendMessageAsync("I got your chat message through event handler class!");
    }
}
