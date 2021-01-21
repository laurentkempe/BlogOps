using System;
using System.IO;
using System.Threading.Tasks;
using BlogOps.Blog;
using BlogOps.Utils;
using CliFx;
using CliFx.Attributes;

namespace BlogOps.Commands
{
    [Command("publish", Description = "Publish a draft.")]
    public class PublishCommand : ICommand
    {
        [CommandParameter(0, Description = "Filename of the draft.")]
        public string Filename { get; set; }

        public async ValueTask ExecuteAsync(IConsole console)
        {
            var draftPath = Path.Combine(BlogSettings.DraftsFolder, Filename);

            var allText = await File.ReadAllTextAsync(draftPath);

            var blogFrontMatter = allText.GetFrontMatter<BlogFrontMatter>();
            
            var date = DateTime.Now;
            var slug = blogFrontMatter.Title.ToUrlSlug();
            
            await console.Output.WriteLineAsync($"{date}/{slug}");
        }
    }
}