using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BlogOps.Blog;
using BlogOps.Utils;
using CliFx;
using CliFx.Attributes;

namespace BlogOps.Commands
{
    [Command("new", Description = "Create a new draft blog post.")]
    public class NewCommand : ICommand
    {
        [CommandParameter(0, Description = "Name of the post, will also be turned into slug for the url.")]
        public string Title { get; set; }
        
        public async ValueTask ExecuteAsync(IConsole console)
        {
            var date = DateTime.Now;
            var slug = Title.ToUrlSlug();
            
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine($"title: '{Title}'");
            stringBuilder.AppendLine($"permalink: /{date:yyyy/MM/dd}/{slug}/");
            stringBuilder.AppendLine($"date: {date}");
            stringBuilder.AppendLine($"disqusIdentifier: {date:yyyyMMddhhmmss}");
            stringBuilder.AppendLine("coverSize: partial");
            stringBuilder.AppendLine("tags: [\"ASP.NET Core\", \"Microsoft Azure\", \"Docker\"]");
            stringBuilder.AppendLine("coverCaption: 'LO Ferré, Petite Anse, Martinique, France'");
            stringBuilder.AppendLine("coverImage: 'https://c7.staticflickr.com/9/8689/16775792438_e45283970c_h.jpg'");
            stringBuilder.AppendLine("thumbnailImage: 'https://c7.staticflickr.com/9/8689/16775792438_8366ee5732_q.jpg'");
            stringBuilder.AppendLine("---");
            stringBuilder.AppendLine("Text displayed on the home page");
            stringBuilder.AppendLine("<!-- more -->");
            stringBuilder.AppendLine("Continue with text displayed on the blog page");
            stringBuilder.AppendLine("![alt image](https://live.staticflickr.com/65535/49566323082_e1817988c2_c.jpg)");
            stringBuilder.AppendLine("{% alert info %}");
            stringBuilder.AppendLine("{% endalert %}");
            stringBuilder.AppendLine("{% codeblock GreeterService.cs lang:csharp %}");
            stringBuilder.AppendLine("{% endcodeblock %}");
            stringBuilder.AppendLine("# Conclusion");
            stringBuilder.AppendLine("TODO");
            stringBuilder.AppendLine("<p></p>");
            stringBuilder.AppendLine("{% githubCard user:laurentkempe repo:dotfiles align:left %}");

            var filename = $"{slug}.md";
            var path = Path.Combine(BlogSettings.DraftsFolder, filename);
            
            await File.WriteAllTextAsync(path, stringBuilder.ToString());
        }
    }
}
