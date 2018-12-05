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
            GenerateStructAndParent(outputPath, NamespaceName, "Layers", "LayerName", NameGetter.GetLayers());
            GenerateStructAndParent(outputPath, NamespaceName, "Scenes", "SceneName", NameGetter.GetScenes());
            GenerateStructAndParent(outputPath, NamespaceName, "SortingLayers", "SortingLayerName",
                NameGetter.GetSortingLayers());

            AssetDatabase.Refresh();
        }

        private static void GenerateStructAndParent(string outputPath, string namespaceName, string className,
            string structName, IEnumerable<NameWithNumber> nameWithNumbers)
        {
            var parentCode = new StaticClassCodeBuilder()
                .AddHeaderCommend(HeaderComment)
                .AddNamespace(namespaceName)
                .AddClass(className)
                .AddObjectParameters(structName, nameWithNumbers)
                .Build();
            CodeSerializer.WriteCodeFile(outputPath, parentCode, className, namespaceName);
        }
    }
}