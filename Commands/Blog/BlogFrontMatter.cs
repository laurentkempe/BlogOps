using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using YamlDotNet.Serialization;

namespace BlogOps.Commands.Blog
{
    [UsedImplicitly]
    public class BlogFrontMatter
    {
        [YamlMember(Alias = "title", Order = 0)]
        public string Title { get; set; }
        
        [YamlMember(Alias = "permalink", Order = 1)]
        public string PermaLink { get; set; }
        
        [YamlMember(Alias = "date", Order = 2)]
        public string Date { get; set; }
        
        [YamlMember(Alias = "disqusIdentifier", Order = 3)]
        public string DisqusIdentifier { get; set; }
        
        [YamlMember(Alias = "tags", Order = 5)]
        public string Tags { get; set; }
    
        [YamlMember(Alias = "coverSize", Order = 4)]
        public string CoverSize { get; set; }
        
        [YamlMember(Alias = "coverCaption", Order = 6)]
        public string CoverCaption { get; set; }
        
        [YamlMember(Alias = "coverImage", Order = 7)]
        public string CoverImage { get; set; }

        [YamlMember(Alias = "thumbnailImage", Order = 8)]
        public string ThumbnailImage { get; set; }

        [YamlIgnore]
        public IList<string> GetTags => Tags?
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray();    }
}