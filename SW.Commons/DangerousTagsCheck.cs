namespace SW
{
    using System;
    using System.Text.RegularExpressions;

    public class DangerousTagsCheck
    {
        private static string[] _dangerousTags = new string[] { "script", "iframe", "frameset", "style" };
        private static RegexOptions options = (RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        private static string regFormat = @"<\s*?{0}.[^>]*?(<.*?{0}.*?>|/\s*?>)|<\s*?{0}.*?>";
        private static string regOnAttr = "<.*?((on|un).[^=<>]*)=.*?>";
        private static string regTagsAttr = @"<.[^>]*?href\s*?.[^>]*?((script)|(\&\#)).[^>]*?>";

        public static string SafeHtml(string html)
        {
            return SafeHtml(html, "<!-- DangerousTag:{0} -->");
        }

        public static string SafeHtml(string html, string replaceWord)
        {
            return SafeHtml(html, replaceWord, null);
        }

        public static string SafeHtml(string html, string replaceWord, string[] dangerousTags)
        {
            if (!string.IsNullOrEmpty(html))
            {
                if ((dangerousTags == null) || (dangerousTags.Length < 1))
                {
                    dangerousTags = _dangerousTags;
                }
                foreach (string str in dangerousTags)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        Regex regex = new Regex(string.Format(regFormat, str), options);
                        foreach (Match match in regex.Matches(html))
                        {
                            string str3 = match.Groups[0].Value;
                            if (!string.IsNullOrEmpty(str3))
                            {
                                html = html.Replace(str3, string.Format(replaceWord, str));
                            }
                        }
                    }
                }
                Regex regex2 = new Regex(regTagsAttr, options);
                foreach (Match match2 in regex2.Matches(html))
                {
                    string str4 = match2.Groups[0].Value;
                    if (!string.IsNullOrEmpty(str4))
                    {
                        html = html.Replace(str4, "<!--  dangerous attribute -->");
                    }
                }
                regex2 = new Regex(regOnAttr, options);
                foreach (Match match3 in regex2.Matches(html))
                {
                    string str5 = match3.Groups[1].Value;
                    if (!string.IsNullOrEmpty(str5))
                    {
                        html = html.Replace(str5, "dangerousAttr");
                    }
                }
            }
            return html;
        }
    }
}

