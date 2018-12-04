#pragma warning disable XS0001

using System.Text;

namespace NamesCodeGenerator.CodeBuilder
{
    public abstract class CodeBuilder
    {
        public const string Indent = "    ";
        protected readonly StringBuilder sb = new StringBuilder();
        int currentDepth = 0;

        public CodeBuilder(string headerComment)
        {
            AppendIndentLine(headerComment);
        }

        public void AddNamespace(string namespaceName)
        {
            sb.AppendLine("namespace " + namespaceName);
            IncreaseIndent();
        }

        protected void IncreaseIndent()
        {
            AppendIndentLine("{");
            currentDepth += 1;
        }

        protected void DecreaseIndent()
        {
            currentDepth -= 1;
            AppendIndentLine("}");
        }

        public string Build()
        {
            while (currentDepth > 0)
                DecreaseIndent();
            return sb.ToString();
        }

        protected void AppendIndentLine(string s)
        {
            for (var i = 0; i < currentDepth; i++)
                sb.Append(Indent);
            sb.AppendLine(s);
        }
    }
}
#pragma warning restore XS0001
