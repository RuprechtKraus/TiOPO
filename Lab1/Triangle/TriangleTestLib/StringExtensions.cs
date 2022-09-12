using System.Text;
using System.Text.RegularExpressions;

namespace TriangleTestLib
{
    public static class StringExtensions
    {
        public static string RemoveNonPrintableCharacters( this string str )
        {
            return Regex.Replace( str, @"\p{C}+", string.Empty );
        }
    }
}
