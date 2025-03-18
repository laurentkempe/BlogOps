namespace BlogOps.Commands.Blog;

public static class BlogSettings
{
    public static string SourceFolder => @".\src";

    public static string DraftsFolder => PostsFolder;

    public static string PostsFolder => SourceFolder + @"\content\posts";
}