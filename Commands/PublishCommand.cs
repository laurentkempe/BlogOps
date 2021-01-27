using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BlogOps.Blog;
using BlogOps.Utils;
using CliFx;
using CliFx.Attributes;
using Spectre.Console;

namespace BlogOps.Commands
{
    [Command("publish", Description = "Publish a draft.")]
    public class PublishCommand : ICommand
    {
        [CommandParameter(0, Description = "Filename of the draft.")]
        public string Filename { get; set; }

        [CommandOption("Overwrite", 'o', Description = "Overwrite file if it exists.")]
        public bool Overwrite { get; set; } = false;

        public async ValueTask ExecuteAsync(IConsole console)
        {
            var destinationPostPath = Path.Combine(BlogSettings.PostsFolder, Filename);

            if (!Overwrite && File.Exists(destinationPostPath))
            {
                await console.Output.WriteLineAsync($"File exists, please use {nameof(Overwrite)} parameter.");

                return;
            }

            var updateDraftLines = await GetUpdatedDraftFrontMatter();

            await File.WriteAllLinesAsync(destinationPostPath, updateDraftLines);
            
            AnsiConsole.Markup($"Published [green]{destinationPostPath}[/]");
        }

        private async Task<string[]> GetUpdatedDraftFrontMatter()
        {
            var sourceDraftPath = Path.Combine(BlogSettings.DraftsFolder, Filename);
            var draftContent = await File.ReadAllTextAsync(sourceDraftPath);

            var draftFrontMatter = draftContent.GetFrontMatter<BlogFrontMatter>();
            var draftContentLines = draftContent.Split(Environment.NewLine);

            UpdateDraftFrontMatter(draftContentLines, draftFrontMatter);

            return draftContentLines;
        }

        private static void UpdateDraftFrontMatter(IList<string> contentLines, BlogFrontMatter blogFrontMatter)
        {
            var now = DateTime.Now;
            var slug = blogFrontMatter.Title.ToUrlSlug();
            
            for (var index = 0; index < contentLines.Count; index++)
            {
                var contentLine = contentLines[index];
                if (contentLine.IsPermalinkLine())
                {
                    contentLines[index] = now.ToPermalink(slug);
                }

                if (contentLine.IsDateLine())
                {
                    contentLines[index] = now.ToDate();
                }

                if (contentLine.IsDisqusIdentifierLine())
                {
                    contentLines[index] = now.ToDisqusIdentifier();
                }
            }
        }
    }
}