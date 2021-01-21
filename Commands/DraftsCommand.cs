using System.IO;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using Spectre.Console;

namespace BlogOps.Commands
{
    [Command("drafts", Description = "List all drafts blog post.")]
    public class DraftsCommand : ICommand
    {
        public ValueTask ExecuteAsync(IConsole console)
        {
            const string draftsFolder = @".\source\_drafts";

            // Create a table
            var table = new Table();

            // Add some columns
            table.AddColumn("Draft name");
            table.AddColumn(new TableColumn("Created").Centered());

            foreach (var file in Directory.GetFiles(draftsFolder))
            {
                var fileInfo = new FileInfo(file);

                // Add some rows
                table.AddRow($"{fileInfo.Name}", $"[green]{fileInfo.CreationTime}[/]");
            }

            // Render the table to the console
            AnsiConsole.Render(table);

            return default;
        }
    }
}