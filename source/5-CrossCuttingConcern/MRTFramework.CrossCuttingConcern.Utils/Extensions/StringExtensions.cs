using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MRTFramework.CrossCuttingConcern.Utils.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Usage: var str = s.ExceptChars(new[] { ' ', '\t', '\n', '\r' });
        /// Or to be even faster:
        /// var str = s.ExceptChars(new HashSet<char>(new[] { ' ', '\t', '\n', '\r' }));
        /// With the hashset version, a string of 11 millions of chars takes less than 700 ms (and I'm in debug mode)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="toExclude"></param>
        /// <returns></returns>
        public static string CharactersRemove(this string str, IEnumerable<char> toExclude)
        {
            var sb = new StringBuilder(str.Length);
            foreach (var c in str)
            {
                if (!toExclude.Contains(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Removes all white spaces
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string WhiteSpaceRemove(this string str)
        {
            var sb = new StringBuilder(str.Length);
            foreach (var c in str)
            {
                if (!char.IsWhiteSpace(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
