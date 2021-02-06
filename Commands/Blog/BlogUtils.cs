using System;
using System.Globalization;

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
    }
}