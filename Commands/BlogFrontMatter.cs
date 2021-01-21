using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace BlogOps.Commands
{
    public class BlogFrontMatter
    {
        [YamlMember(Alias = "tags")]
        public string Tags { get; set; }
    
        [YamlMember(Alias = "title")]
        public string Title { get; set; }
        
        [YamlMember(Alias = "permalink")]
        public string PermaLink { get; set; }
        
        [YamlMember(Alias = "date")]
        public string Date { get; set; }
        
        [YamlMember(Alias = "disqusIdentifier")]
        public string DisqusIdentifier { get; set; }
        
        [YamlMember(Alias = "coverSize")]
        public string CoverSize { get; set; }
        
        [YamlMember(Alias = "coverCaption")]
        public string CoverCaption { get; set; }
        
        [YamlMember(Alias = "coverImage")]
        public string CoverImage { get; set; }

        [YamlIgnore]
        public IList<string> GetTags => Tags?
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToArray();    }
}