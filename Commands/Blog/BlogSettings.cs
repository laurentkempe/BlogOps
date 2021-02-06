namespace BlogOps.Commands.Blog
{
    public static class BlogSettings
    {
        public static string SourceFolder => @".\source";
        public static string DraftsFolder => SourceFolder + @"\_drafts";

        public static string PostsFolder => SourceFolder + @"\_posts";
    }
}