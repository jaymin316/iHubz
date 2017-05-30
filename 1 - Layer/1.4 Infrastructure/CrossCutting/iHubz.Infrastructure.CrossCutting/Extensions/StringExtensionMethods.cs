using System.Text.RegularExpressions;
using iHubz.Infrastructure.CrossCutting.Defaults;

namespace iHubz.Infrastructure.CrossCutting.Extensions
{
    public static class StringExtensionMethods
    {
        /// <summary>
        /// Helper method that returns true if any unsafe characters are found in the imput text provided 
        /// </summary>
        /// <param name="s">Input String that needs to be checked</param>
        /// <returns>True if input is Safe, false otherwise</returns>
        public static bool IsSafeInput(this string s)
        {
            if (s == null)
                return true;

            return !Regex.IsMatch(s, ApplicationDefaults.RegExHasUnsafeCharacters);
        }
    }
}
