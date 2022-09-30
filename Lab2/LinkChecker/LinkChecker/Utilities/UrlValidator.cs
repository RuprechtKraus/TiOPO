
using System.Text.RegularExpressions;

namespace LinkChecker.Utilities
{
    public static class UrlValidator
    {
        private static readonly Regex UrlPattern = new( "^https?:\\/\\/(?:www\\.)" +
            "?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b" +
            "(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$" );

        public static bool IsValidUrl( string url )
        {
            return UrlPattern.IsMatch( url );
        }
    }
}
