using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BlogOps.Commands.Utils
{
    public static class UrlSlugger
    {
        // white space, em-dash, en-dash, underscore
        static readonly Regex WordDelimiters = new Regex(@"[\s—–_]", RegexOptions.Compiled);

        // characters that are not valid
        static readonly Regex InvalidChars = new Regex(@"[^a-z0-9\-]", RegexOptions.Compiled);

        // multiple hyphens
        static readonly Regex MultipleHyphens = new Regex(@"-{2,}", RegexOptions.Compiled);

        private static readonly IDictionary<string, string> Keywords =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"c#", "csharp"},
                {".net", "dotnet"},
                {"asp.net", "aspnet"}
            };

        public static string ToUrlSlug(this string title)
        {
            var slug = title;
            
            // slug clean up for pesky words
            foreach (var (keyword, replacement) in Keywords) {
                slug = slug.Replace(keyword, replacement, StringComparison.OrdinalIgnoreCase);
            }
            
            // convert to lower case
            slug = slug.ToLowerInvariant();

            // remove diacritics (accents)
            slug = RemoveDiacritics(slug);

            // ensure all word delimiters are hyphens
            slug = WordDelimiters.Replace(slug, "-");

            // strip out invalid characters
            slug = InvalidChars.Replace(slug, "");

            // replace multiple hyphens (-) with a single hyphen
            slug = MultipleHyphens.Replace(slug, "-");

            // trim hyphens (-) from ends
            return slug.Trim('-');
        }
            
        /// See: http://www.siao2.com/2007/05/14/2629747.aspx
        private static string RemoveDiacritics(string stIn)
        {
            var stFormD = stIn.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (var ich = 0; ich < stFormD.Length; ich++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}