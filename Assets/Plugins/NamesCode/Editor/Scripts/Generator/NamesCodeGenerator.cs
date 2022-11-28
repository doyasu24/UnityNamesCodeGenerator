using System;
using System.Collections.Generic;
using System.Linq;
using NamesCode.Generator.CodeBuilder;
using NamesCode.Settings;
using UnityEditor;
using UnityEngine;

namespace NamesCode.Generator
{
    public static class NamesCodeGenerator
    {
        private const string HeaderComment = "// Generated code by NamesCodeGenerator\n";
        private const string NamespaceName = "NamesCode";
        private const string DocumentUrl = "https://github.com/doyasu24/UnityNamesCodeGenerator";

        [MenuItem("Tools/NamesCode/Generate")]
        public static void Generate()
        {
            var generatorSetting = FindGeneratorSetting();
            Generate(generatorSetting);
            Debug.Log($"Successfully generated NamesCode in {generatorSetting.OutputDirectory}");
        }

        private static GeneratorSetting FindGeneratorSetting()
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(GeneratorSetting)}");
            if (guids.Length == 0)
            {
                Debug.LogWarning(
                    $"GeneratorSetting file not found. Default values will be used for code generation.\nsee {DocumentUrl}");
                return GeneratorSetting.Default;
            }

            var assetPaths = guids.Select(AssetDatabase.GUIDToAssetPath).ToArray();
            if (assetPaths.Length > 1) throw MultipleGeneratorSettingException.From(assetPaths);

            return AssetDatabase.LoadAssetAtPath<GeneratorSetting>(assetPaths[0]);
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

    public class MultipleGeneratorSettingException : Exception
    {
        public readonly string[] AssetPaths;

        private MultipleGeneratorSettingException(string message, string[] assetPaths) : base(message)
        {
            AssetPaths = assetPaths;
        }

        public static MultipleGeneratorSettingException From(string[] assetPaths)
        {
            var message =
                $"Multiple GeneratorSetting files found. Only one GeneratorSetting file should exist in the project.\n{string.Join("\n", assetPaths)}";
            return new MultipleGeneratorSettingException(message, assetPaths);
        }
    }
}