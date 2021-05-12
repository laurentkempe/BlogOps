using System.Linq;
using System.Threading.Tasks;
using BlogOps.Commands.Blog;
using CliFx;
using CliFx.Attributes;
using JetBrains.Annotations;
using Spectre.Console;

namespace BlogOps.Commands
{
    [Command("drafts", Description = "List all drafts blog post.")]
    [UsedImplicitly]
    public class DraftsCommand : ICommand
    {
        public async ValueTask ExecuteAsync(IConsole console)
        {
            var table = new Table();

            table.AddColumn("Draft filename");
            table.AddColumn("Title");
            table.AddColumn(new TableColumn("Created").Centered());
            table.AddColumn(new TableColumn("Tags").Centered());
            table.AddColumn(new TableColumn("Permalink").Centered());

            await foreach (var (fileInfo, draftFrontMatter) in BlogUtils.GetDraftInfos())
            {
                table.AddRow($"{fileInfo.Name}", $"{draftFrontMatter.Title}", $"[green]{draftFrontMatter.Date}[/]", $"{string.Join(", ", draftFrontMatter.Tags)}", $"{draftFrontMatter.PermaLink}");
            }

            AnsiConsole.Render(table);
        }
    }
}