using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BPOSolution.Helper
{
    public static class PascalConverterHelper
    {
        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        }

        public static string ToPascalCase(this string s)
        {
            var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(word => word.Substring(0, 1).ToUpper() +
                                         word.Substring(1).ToLower());

            var result = String.Concat(words);
            return result;
        }
    }
}
