using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogOps.Commands.Blog;
using BlogOps.Commands.Utils;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using JetBrains.Annotations;
using Spectre.Console;

namespace BlogOps.Commands;

[Command("publish", Description = "Publish a draft.")]
[UsedImplicitly]
public class PublishCommand : ICommand
{
    [CommandOption("Overwrite", 'o', Description = "Overwrite file if it exists.")]
    public bool Overwrite { get; init; } = false;

    public async ValueTask ExecuteAsync(IConsole console)
    {
        var (draftFileInfo, draftFrontMatter) = await BlogUtils.AskUserToSelectDraft("Which draft do you want to publish?");

        var updatedDraftLines = await GetUpdatedDraftFrontMatterLines(draftFileInfo.FullName, draftFrontMatter);

        await File.WriteAllLinesAsync(draftFileInfo.FullName, updatedDraftLines);
            
        AnsiConsole.Markup($"Published [green]{draftFileInfo.FullName}[/]");
    }

    private static async Task<List<string>> GetUpdatedDraftFrontMatterLines(string fileInfoFullName, BlogFrontMatter draftFrontMatter)
    {
        var draftContent = await File.ReadAllTextAsync(fileInfoFullName);

        var draftContentLines = draftContent.Split(Environment.NewLine).ToList();

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

            if (contentLine.IsDraftLine())
            {
                contentLines.RemoveAt(index);
            }
        }
    }
}