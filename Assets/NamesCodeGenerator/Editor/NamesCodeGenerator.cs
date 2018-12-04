using System.Collections.Generic;
using NamesCodeGenerator.CodeBuilder;
using UnityEditor;

namespace NamesCodeGenerator
{
    public static class NamesCodeGenerator
    {
        const string headerComment = "// Generated code by NamesCodeGenerator\nusing NamesCode;\n";

        public static void GenerateNamesCodes(string outputPath, string namespaceName)
        {
            CodeSerializer.ResetDirectory(outputPath);

            GenerateStructAndParent(outputPath, namespaceName, "Tags", "TagName", NameGetter.GetTags());
            GenerateStructAndParent(outputPath, namespaceName, "Layers", "LayerName", new[] { Member.StringName, Member.IntIndex }, NameGetter.GetLayers());
            GenerateStructAndParent(outputPath, namespaceName, "Scenes", "SceneName", new[] { Member.StringName, Member.IntIndex }, NameGetter.GetScenes());
            GenerateStructAndParent(outputPath, namespaceName, "SortingLayers", "SortingLayerName", new[] { Member.StringName, Member.IntId }, NameGetter.GetSortingLayers());

            AssetDatabase.Refresh();
        }

        static void GenerateStructAndParent(string outputPath, string namespaceName, string parentName, string structName, string[] names)
        {
            var parentCode = GenerateStaticClassCode(namespaceName, parentName, structName, names);
            CodeSerializer.WriteCodeFile(outputPath, parentCode, parentName, namespaceName);
        }

        static string GenerateStaticClassCode(string namespaceName, string className, string structName, string[] names)
        {
            var parentCodeBuilder = new StaticClassCodeBuilder(headerComment);
            if (namespaceName != null)
                parentCodeBuilder.AddNamespace(namespaceName);
            parentCodeBuilder.AddClass(className);
            parentCodeBuilder.AddObjectParameters(structName, names);
            return parentCodeBuilder.Build();
        }

        static string GenerateStaticClassCode(string namespaceName, string className, string structName, IEnumerable<NameWithNumber> nameWithNumbers)
        {
            var parentCodeBuilder = new StaticClassCodeBuilder(headerComment);
            if (namespaceName != null)
                parentCodeBuilder.AddNamespace(namespaceName);
            parentCodeBuilder.AddClass(className);
            parentCodeBuilder.AddObjectParameters(structName, nameWithNumbers);
            return parentCodeBuilder.Build();
        }

        static void GenerateStructAndParent(string outputPath, string namespaceName, string parentName, string structName, Member[] members, IEnumerable<NameWithNumber> nameWithNumbers)
        {
            var parentCode = GenerateStaticClassCode(namespaceName, parentName, structName, nameWithNumbers);
            CodeSerializer.WriteCodeFile(outputPath, parentCode, parentName, namespaceName);
        }
    }
}
