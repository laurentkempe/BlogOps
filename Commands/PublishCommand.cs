using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BlogOps.Commands.Blog;
using BlogOps.Commands.Utils;
using CliFx;
using CliFx.Attributes;
using JetBrains.Annotations;
using Spectre.Console;

namespace BlogOps.Commands
{
    [Command("publish", Description = "Publish a draft.")]
    [UsedImplicitly]
    public class PublishCommand : ICommand
    {
        [CommandOption("Overwrite", 'o', Description = "Overwrite file if it exists.")]
        public bool Overwrite { get; set; } = false;

        public async ValueTask ExecuteAsync(IConsole console)
        {
            var (draftFileInfo, draftFrontMatter) = await BlogUtils.AskUserToSelectDraft("Which draft do you want to publish?");

            var blogPostPath = Path.Combine(BlogSettings.PostsFolder, draftFileInfo.Name);

            if (!Overwrite && File.Exists(blogPostPath))
            {
                await console.Output.WriteLineAsync($"File exists, please use {nameof(Overwrite)} parameter.");

                return;
            }

            var updatedDraftLines = await GetUpdatedDraftFrontMatterLines(draftFileInfo.FullName, draftFrontMatter);

            await File.WriteAllLinesAsync(blogPostPath, updatedDraftLines);
            File.Delete(draftFileInfo.FullName);
            
            AnsiConsole.Markup($"Published [green]{blogPostPath}[/]");
        }

        private async Task<string[]> GetUpdatedDraftFrontMatterLines(string fileInfoFullName, BlogFrontMatter draftFrontMatter)
        {
            var sourceDraftPath = Path.Combine(BlogSettings.DraftsFolder, fileInfoFullName);
            var draftContent = await File.ReadAllTextAsync(sourceDraftPath);

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