using System.Collections.Generic;
using NamesCode.Generator.CodeBuilder;
using UnityEditor;

namespace NamesCode.Generator
{
    public static class NamesCodeGenerator
    {
        private const string HeaderComment = "// Generated code by NamesCodeGenerator";
        private const string NamespaceName = "NamesCode";
        
        public static void GenerateNamesCodes(string outputPath)
        {
            CodeSerializer.ResetDirectory(outputPath);

            GenerateStructAndParent(outputPath, NamespaceName, "Tags", "TagName", NameGetter.GetTags());
            GenerateStructAndParent(outputPath, NamespaceName, "Layers", "LayerName", new[] { Member.StringName, Member.IntIndex }, NameGetter.GetLayers());
            GenerateStructAndParent(outputPath, NamespaceName, "Scenes", "SceneName", new[] { Member.StringName, Member.IntIndex }, NameGetter.GetScenes());
            GenerateStructAndParent(outputPath, NamespaceName, "SortingLayers", "SortingLayerName", new[] { Member.StringName, Member.IntId }, NameGetter.GetSortingLayers());

            AssetDatabase.Refresh();
        }

        static void GenerateStructAndParent(string outputPath, string namespaceName, string parentName, string structName, string[] names)
        {
            var parentCode = GenerateStaticClassCode(namespaceName, parentName, structName, names);
            CodeSerializer.WriteCodeFile(outputPath, parentCode, parentName, namespaceName);
        }

        static string GenerateStaticClassCode(string namespaceName, string className, string structName, string[] names)
        {
            var parentCodeBuilder = new StaticClassCodeBuilder(HeaderComment);
            if (namespaceName != null)
                parentCodeBuilder.AddNamespace(namespaceName);
            parentCodeBuilder.AddClass(className);
            parentCodeBuilder.AddObjectParameters(structName, names);
            return parentCodeBuilder.Build();
        }

        static string GenerateStaticClassCode(string namespaceName, string className, string structName, IEnumerable<NameWithNumber> nameWithNumbers)
        {
            var parentCodeBuilder = new StaticClassCodeBuilder(HeaderComment);
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
