using System.Collections.Generic;
using System.IO;
using NamesCode.Generator.CodeBuilder;
using NamesCode.Settings;
using UnityEditor;

namespace NamesCode.Generator
{
    public static class NamesCodeGenerator
    {
        private const string HeaderComment = "// Generated code by NamesCodeGenerator";
        private const string NamespaceName = "NamesCode";
        
        [MenuItem("Tools/NamesCode/Generate %g")]
        public static void Generate()
        {
            var setting = AssetDatabase.LoadAssetAtPath<GeneratorSetting>(GeneratorSetting.Path);
            if (setting == null) throw new FileNotFoundException("GeneratorSetting not found: " + GeneratorSetting.Path);
            Generate(setting);
        }

        private static void Generate(GeneratorSetting setting)
        {
            CodeSerializer.ResetDirectory(setting.OutputDirectory);

            GenerateStructAndParent(setting.OutputDirectory, "Tags", "TagName", NameGetter.GetTags());
            GenerateStructAndParent(setting.OutputDirectory, "Layers", "LayerName", NameGetter.GetLayers());
            GenerateStructAndParent(setting.OutputDirectory, "Scenes", "SceneName", NameGetter.GetScenes());
            GenerateStructAndParent(setting.OutputDirectory, "SortingLayers", "SortingLayerName",
                NameGetter.GetSortingLayers());

            AssetDatabase.Refresh();
        }

        private static void GenerateStructAndParent(string outputPath, string className, string structName,
            IEnumerable<NameWithNumber> nameWithNumbers)
        {
            var code = new StaticClassCodeBuilder()
                .AddHeaderCommend(HeaderComment)
                .AddNamespace(NamespaceName)
                .AddClass(className)
                .AddObjectParameters(structName, nameWithNumbers)
                .Build();
            CodeSerializer.WriteCodeFile(outputPath, code, className);
        }
    }
}