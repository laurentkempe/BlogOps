using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using JetBrains.Annotations;
using SimpleExec;

namespace BlogOps.Commands;

[Command("server", Description = "Start local server showing optionally draft blog posts.")]
[UsedImplicitly]
public class ServerCommand : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        await Command.RunAsync("pnpm", $"run dev", "./");
    }
}