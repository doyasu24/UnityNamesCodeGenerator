using System.Collections.Generic;
using System.Linq;

namespace NamesCodeGenerator.CodeBuilder
{
    public class StaticClassCodeBuilder : CodeBuilder
    {
        public StaticClassCodeBuilder(string headerComment) : base(headerComment) { }

        public void AddClass(string className)
        {
            AppendIndentLine(string.Format("public static class {0}", className));
            IncreaseIndent();
        }

        public void AddObjectParameters(string typeName, IEnumerable<string> names)
        {
            var parameters = names.Select(n => GenerateStructProperty(typeName, Utils.ConvertToVariableName(n), Utils.SurroundWithDoubleQuote(n)));
            foreach (var parameter in parameters)
                AppendIndentLine(parameter);

            if (parameters.Any())
                sb.AppendLine();
            
            var variables = string.Join(", ", names.Select(Utils.ConvertToVariableName).ToArray());
            var arrayParameter = string.Format("public static readonly {0}[] Names = {1} {2} {3};", typeName, "{", variables, "}");
            AppendIndentLine(arrayParameter);
        }

        public void AddObjectParameters(string typeName, IEnumerable<NameWithNumber> nameWithNumbers)
        {
            var parameters = nameWithNumbers.Select(n => GenerateStructProperty(typeName, Utils.ConvertToVariableName(n.Name), Utils.SurroundWithDoubleQuote(n.Name), n.Number.ToString()));
            foreach (var parameter in parameters)
                AppendIndentLine(parameter);

            if (parameters.Any())
                sb.AppendLine();

            var variables = string.Join(", ", nameWithNumbers.Select(n => n.Name).Select(Utils.ConvertToVariableName).ToArray());
            var arrayParameter = string.Format("public static readonly {0}[] Names = {1} {2} {3};", typeName, "{", variables, "}");
            AppendIndentLine(arrayParameter);
        }

        static string GenerateStructProperty(string typeName, string variableName, params string[] constructorArguments)
        {
            var args = string.Join(", ", constructorArguments);
            return string.Format("public static readonly {0} {1} = new {0}({2});", typeName, variableName, args);
        }
    }
}
