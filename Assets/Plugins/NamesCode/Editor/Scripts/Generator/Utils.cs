using System.Text.RegularExpressions;

namespace NamesCode.Generator
{
    public static class Utils
    {
        public static string SurroundWithDoubleQuote(string str)
        {
            return "\"" + str + "\"";
        }

        public static string ConvertToVariableName(string name)
        {
            // replace unusable characters with "_"
            var result = Regex.Replace(name, "[^a-zA-Z0-9]", "_");
            if (char.IsDigit(result[0]))
                return result.Insert(0, "_");
            return result;
        }
    }
}