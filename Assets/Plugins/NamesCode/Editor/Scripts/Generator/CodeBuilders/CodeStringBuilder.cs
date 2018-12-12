#pragma warning disable XS0001

using System.Text;

namespace NamesCode.Generator.CodeBuilder
{
    public class CodeStringBuilder
    {
        private const string Indent = "    ";
        private readonly StringBuilder _sb = new StringBuilder();
        private int _currentDepth;

        public void AddHeaderCommend(string headerComment)
        {
            AppendIndentLine(headerComment);
        }

        public void AddNamespace(string namespaceName)
        {
            _sb.AppendLine("namespace " + namespaceName);
            IncreaseIndent();
        }

        public void AddEmptyLine()
        {
            _sb.AppendLine();
        }

        public void IncreaseIndent()
        {
            AppendIndentLine("{");
            _currentDepth += 1;
        }

        public void DecreaseIndent()
        {
            _currentDepth -= 1;
            AppendIndentLine("}");
        }

        public void CloseIndent()
        {
            _currentDepth -= 1;
            AppendIndentLine("};");
        }

        public string Build()
        {
            while (_currentDepth > 0)
                DecreaseIndent();
            return _sb.ToString();
        }

        public void AppendIndentLine(string s)
        {
            for (var i = 0; i < _currentDepth; i++)
                _sb.Append(Indent);
            _sb.AppendLine(s);
        }
    }
}
#pragma warning restore XS0001
