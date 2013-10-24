﻿using System.Text.RegularExpressions;

namespace AmplaWeb.Data.Binding.MetaData
{
    public static class DisplayStringExtensions
    {
        private static readonly Regex SeparateWordsRegex = new Regex("([A-Z][a-z])", RegexOptions.Compiled);

        public static string ToSeparatedWords(this string value)
        {
            return SeparateWordsRegex.Replace(value, " $1").Trim();
        }
    }
}