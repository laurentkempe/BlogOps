using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using JetBrains.Annotations;
using SimpleExec;

namespace BlogOps.Commands
{
    [Command("server", Description = "Start local server showing optionally draft blog posts.")]
    [UsedImplicitly]
    public class ServerCommand : ICommand
    {
        [CommandOption("draft", 'd', Description = "With draft.")]
        public bool Draft { get; set; } = false;

        public async ValueTask ExecuteAsync(IConsole console)
        {
            var draft = Draft ? "--draft" : "";
            
            await Command.RunAsync("hexo.cmd", $"server --open {draft}", "./");
        }
    }
}