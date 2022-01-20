using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Extensions
{
    public static class StringExtension
    {
        public static bool HasUnicodeCharacter(this string input) => !Regex.IsMatch(input, @"^[\p{L} \.\-]+$");

        public static string NormalizePDFName(this string pdfName)
        {
            var characters = new string[] { "/", @"\\", "\n", "\t", "\r", " "};
            var name = pdfName;
            foreach (var item in characters)
            {
                name = name.Replace(item, "");
            }

            return name;
        }
    }
}
