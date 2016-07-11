using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string str1, string str2)
        {
            return string.Equals(str1, str2, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string Capitalize(this string originalString)
        {
            var capitalizedString = string.Concat(originalString.Substring(0, 1).ToUpper(), originalString.Substring(1));

            return capitalizedString;
        }
    }
}