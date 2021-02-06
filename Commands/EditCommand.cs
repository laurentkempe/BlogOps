using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BlogOps.Commands.Blog;
using CliFx;
using CliFx.Attributes;
using JetBrains.Annotations;
using SimpleExec;
using Spectre.Console;

namespace BlogOps.Commands
{
    [Command("edit", Description = "Edit a drafts blog post.")]
    [UsedImplicitly]
    public class EditCommand : ICommand
    {
        public async ValueTask ExecuteAsync(IConsole console)
        {
            var drafts = new Dictionary<string, (FileInfo fileInfo, BlogFrontMatter draftFrontMatter)>();

            await foreach (var draftInfo in BlogUtils.GetDraftInfos())
            {
                drafts.Add(draftInfo.draftFrontMatter.Title, draftInfo);
            }

            var draft = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Which draft do you want to edit?")
                    .PageSize(10)
                    .AddChoices(drafts.Keys));

            AnsiConsole.WriteLine($"You selected '{draft}'!");
            
            await Command.RunAsync("cmd.exe", $"/c code {drafts[draft].fileInfo.FullName}", "./");
        }
    }
}