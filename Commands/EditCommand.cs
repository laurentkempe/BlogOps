using System.Threading.Tasks;
using BlogOps.Commands.Blog;
using CliFx;
using CliFx.Attributes;
using JetBrains.Annotations;
using SimpleExec;

namespace BlogOps.Commands
{
    [Command("edit", Description = "Edit a drafts blog post.")]
    [UsedImplicitly]
    public class EditCommand : ICommand
    {
        public async ValueTask ExecuteAsync(IConsole console)
        {
            var (fileInfo, _) = await BlogUtils.AskUserToSelectDraft("Which draft do you want to edit?");

            await Command.RunAsync("cmd.exe", $"/c code {fileInfo.FullName}", "./");
        }
    }
}