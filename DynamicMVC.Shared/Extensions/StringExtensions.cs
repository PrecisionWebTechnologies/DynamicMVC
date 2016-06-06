using System.Collections.Generic;

namespace DynamicMVC.Shared.Extensions
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitAndTrim(this string str)
        {
            return str.Replace(" ","").Split(',');
        }
    }
}
