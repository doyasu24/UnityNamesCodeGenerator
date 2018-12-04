#pragma warning disable XS0001

using System.IO;
using System.Text;
using System.Linq;

namespace NamesCodeGenerator
{
    public static class ExtensionCodeGenerator
    {
        const string templateDirectoryPath = "Assets/NamesCodeGenerator/Editor/Extensions";
        const string templateCodeExtension = ".cst";

        static string AddNamespace(string templateText, string namespaceName)
        {
            var lines = templateText.Split('\n');
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("namespace {0}", namespaceName));
            sb.AppendLine("{");
            foreach (var l in lines)
            {
                sb.AppendLine(CodeBuilder.CodeBuilder.Indent + l);
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static void GenerateExtensionCodes(string outputPath, string namespaceName)
        {
            var files = Directory.GetFiles(templateDirectoryPath)
                                 .Where(f => f.EndsWith(templateCodeExtension, System.StringComparison.Ordinal));
            foreach (var file in files)
            {
                var className = Path.GetFileNameWithoutExtension(file);
                var code = File.ReadAllText(file);
                if(namespaceName != null)
                    code = AddNamespace(code, namespaceName);
                CodeSerializer.WriteCodeFile(outputPath, code, className, namespaceName);
            }
        }
    }
}

#pragma warning restore XS0001