using System;

namespace SendMailExchange.Extensions
{
    public static class StringExtensions
    {
        public static string ToHtmlNewLines(this string s)
        {
            return s.Replace(Environment.NewLine, "<br/>");
        }
    }
}
