using System.IO;
using System.Threading.Tasks;
using BlogOps.Blog;
using BlogOps.Utils;
using CliFx;
using CliFx.Attributes;
using Spectre.Console;

namespace BlogOps.Commands
{
    [Command("drafts", Description = "List all drafts blog post.")]
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

            foreach (var draftPath in Directory.GetFiles(BlogSettings.DraftsFolder))
            {
                var (fileInfo, draftFrontMatter) = await GetDraftInfo(draftPath);

                table.AddRow($"{fileInfo.Name}", $"{draftFrontMatter.Title}", $"[green]{draftFrontMatter.Date}[/]", $"{draftFrontMatter.Tags}", $"{draftFrontMatter.PermaLink}");
            }

            AnsiConsole.Render(table);
        }

        private static async Task<(FileInfo fileInfo, BlogFrontMatter draftFrontMatter)> GetDraftInfo(string draftPath)
        {
            var fileInfo = new FileInfo(draftPath);
            var draftFrontMatter = await GetDraftFrontMatter(draftPath);

            return (fileInfo, draftFrontMatter);
        }

        private static async Task<BlogFrontMatter> GetDraftFrontMatter(string draftPath)
        {
            var allText = await File.ReadAllTextAsync(draftPath);

            return allText.GetFrontMatter<BlogFrontMatter>();
        }
    }
}