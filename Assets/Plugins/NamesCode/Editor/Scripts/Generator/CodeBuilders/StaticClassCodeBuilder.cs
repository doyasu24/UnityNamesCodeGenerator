using System.Collections.Generic;
using System.Linq;

namespace NamesCode.Generator.CodeBuilder
{
    public class StaticClassCodeBuilder
    {
        private readonly CodeStringBuilder _builder = new CodeStringBuilder();

        public StaticClassCodeBuilder AddHeaderCommend(string headerComment)
        {
            _builder.AddHeaderCommend(headerComment);
            return this;
        }

        public StaticClassCodeBuilder AddNamespace(string namespaceName)
        {
            _builder.AddNamespace(namespaceName);
            return this;
        }

        public StaticClassCodeBuilder AddClass(string className)
        {
            _builder.AppendIndentLine(string.Format("public static class {0}", className));
            _builder.IncreaseIndent();
            return this;
        }

        public StaticClassCodeBuilder AddObjectParameters(string typeName, string[] names)
        {
            var parameters = names.Select(n => GenerateStructProperty(typeName, Utils.ConvertToVariableName(n), Utils.SurroundWithDoubleQuote(n)));
            foreach (var parameter in parameters)
                _builder.AppendIndentLine(parameter);

            if (parameters.Any())
                _builder.AddEmptyLine();
            
            var variables = string.Join(", ", names.Select(Utils.ConvertToVariableName).ToArray());
            var arrayParameter = string.Format("public static readonly {0}[] Names = {1} {2} {3};", typeName, "{", variables, "}");
            _builder.AppendIndentLine(arrayParameter);
            return this;
        }

        public StaticClassCodeBuilder AddObjectParameters(string typeName, IEnumerable<NameWithNumber> nameWithNumbers)
        {
            var parameters = nameWithNumbers.Select(n => GenerateStructProperty(typeName, Utils.ConvertToVariableName(n.Name), Utils.SurroundWithDoubleQuote(n.Name), n.Number.ToString()));
            foreach (var parameter in parameters)
                _builder.AppendIndentLine(parameter);

            if (parameters.Any())
                _builder.AddEmptyLine();

            var variables = string.Join(", ", nameWithNumbers.Select(n => n.Name).Select(Utils.ConvertToVariableName).ToArray());
            var arrayParameter = string.Format("public static readonly {0}[] Names = {1} {2} {3};", typeName, "{", variables, "}");
            _builder.AppendIndentLine(arrayParameter);
            return this;
        }

        public string Build()
        {
            return _builder.Build();
        }

        private static string GenerateStructProperty(string typeName, string variableName, params string[] constructorArguments)
        {
            var args = string.Join(", ", constructorArguments);
            return string.Format("public static readonly {0} {1} = new {0}({2});", typeName, variableName, args);
        }
    }
}
