using System.Text.RegularExpressions;

namespace NamesCode.Generator
{
    public static class CodeStringUtils
    {
        public static string SurroundWithDoubleQuote(string str)
        {
            return "\"" + str + "\"";
        }

        public static string ConvertToVariableName(string name)
        {
            var spaceRemoved = name.Replace(" ", "");

            // replace unusable characters with "_"
            var result = Regex.Replace(spaceRemoved, "[^a-zA-Z0-9]", "_");
            return char.IsDigit(result[0]) ? result.Insert(0, "_") : result;
        }
    }
}