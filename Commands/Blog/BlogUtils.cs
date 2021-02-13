using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using BlogOps.Commands.Utils;
using Spectre.Console;

namespace BlogOps.Commands.Blog
{
    public static class BlogUtils
    {
        private const string Permalink = "permalink:";
        private const string Disqusidentifier = "disqusIdentifier:";
        private const string Date = "date:";

        public static bool IsPermalinkLine(this string line) => line.StartsWith(Permalink);
        public static string ToPermalink(this DateTime date, string slug) => $"{Permalink} /{date:yyyy/MM/dd}/{slug}/";

        public static bool IsDisqusIdentifierLine(this string contentLine) => contentLine.StartsWith(Disqusidentifier);
        public static string ToDisqusIdentifier(this DateTime date) => $"{Disqusidentifier} {date:yyyyMMddhhmmss}";

        public static bool IsDateLine(this string contentLine) => contentLine.StartsWith(Date);
        public static string ToDate(this DateTime date) => $"{Date} {date.ToString(CultureInfo.InvariantCulture)}";

        public static async IAsyncEnumerable<(FileInfo fileInfo, BlogFrontMatter draftFrontMatter)> GetDraftInfos()
        {
            foreach (var draftPath in Directory.GetFiles(BlogSettings.DraftsFolder))
            {
                yield return await GetDraftInfo(draftPath);
            }            
        }

        public static async Task<(FileInfo fileInfo, BlogFrontMatter draftFrontMatter)> AskUserToSelectDraft(string userQuestion)
        {
            var drafts = new Dictionary<string, (FileInfo fileInfo, BlogFrontMatter draftFrontMatter)>();

            await foreach (var draftInfo in GetDraftInfos())
            {
                drafts.Add(draftInfo.draftFrontMatter.Title, draftInfo);
            }

            var draft = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(userQuestion)
                    .PageSize(10)
                    .AddChoices(drafts.Keys));

            AnsiConsole.WriteLine($"You selected '{draft}'!");

            return drafts[draft];
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