using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BlogOps.Commands.Blog;
using BlogOps.Commands.Utils;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using JetBrains.Annotations;
using Spectre.Console;

namespace BlogOps.Commands;

[Command("new", Description = "Create a new draft blog post.")]
[UsedImplicitly]
public class NewCommand : ICommand
{
    [CommandParameter(0, Description = "Name of the post, will also be turned into slug for the url.")]
    public string Title { get; set; }
        
    public async ValueTask ExecuteAsync(IConsole console)
    {
        var now = DateTime.Now;
        var slug = Title.ToUrlSlug();
        
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($$"""
                                   ---
                                   title: '{{Title}}'
                                   {{now.ToPermalink(slug)}}
                                   date: {{now}}
                                   {{now.ToDisqusIdentifier()}}
                                   coverSize: partial
                                   tags: [AI, ASP.NET Core, C#, LLM, SLM, Docker]
                                   coverCaption: 'LO Ferré, Petite Anse, Martinique, France'
                                   coverImage: 'https://c7.staticflickr.com/9/8689/16775792438_e45283970c_h.jpg'
                                   thumbnailImage: 'https://c7.staticflickr.com/9/8689/16775792438_8366ee5732_q.jpg'
                                   draft: true
                                   ---
                                   Text displayed on the home page
                                   {/* <!-- more --> */}
                                   # Introduction
                                   # Requirements
                                   Continue with text displayed on the blog page
                                   ![alt image](https://live.staticflickr.com/65535/49566323082_e1817988c2_c.jpg)
                                   ![alt iamge](/images/2025/dotnet-aspire_jetbrains-rider-services.png)
                                   <Alert mode="warning">
                                   This is a warning.
                                   </Alert>
                                   <Alert mode="info">
                                   This is a info.
                                   </Alert>
                                   ```csharp
                                   var now = DateTime.Now;
                                   var slug = Title.ToUrlSlug();
                                   ```
                                   # Conclusion
                                   TODO
                                   # References
                                   * [GitHub](https://github.com/laurentkempe/aiPlayground)
                                   
                                   Get the source code on GitHub [laurentkempe/aiPlayground/OllamaMCPServerMicrosoftExtensions](TODO).
                                   
                                   <GitHubCard user="laurentkempe" repo="aiPlayground" />
                                   """);

        var filename = $"{slug}.mdx";
        var path = Path.Combine(BlogSettings.PostsFolder, DateTime.Now.Year.ToString(), filename);
        
        await File.WriteAllTextAsync(path, stringBuilder.ToString());
        
        AnsiConsole.Markup($"Successfully created [green]{filename}[/]");
    }}