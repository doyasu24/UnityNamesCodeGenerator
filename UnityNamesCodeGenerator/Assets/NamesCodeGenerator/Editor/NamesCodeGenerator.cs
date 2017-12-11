using System.Linq;
using System.IO;
using UnityEditor;

namespace NamesCodeGenerator
{
    public static class NamesCodeGenerator
    {
        public static void GenerateConstStaticClasses(string outputPath, string namespaceName = null)
        {
            DeleteDirectoryIfExists(outputPath, true);
            CreateDirectoryIfNotExists(outputPath);

            // Generate TagName
            var tagCode = CodeGenerator.GenerateConstClass("TagName", NameGetter.GetTags());
            Generate(outputPath, tagCode, "TagName", namespaceName);

            // Generate LayerName
            var layerCode = CodeGenerator.GenerateConstClass("LayerName", NameGetter.GetLayers().Select(n => n.Name).ToArray());
            Generate(outputPath, layerCode, "LayerName", namespaceName);

            // Generate SceneName
            var sceneCode = CodeGenerator.GenerateConstClass("SceneName", NameGetter.GetScenes().Select(n => n.Name).ToArray());
            Generate(outputPath, sceneCode, "SceneName", namespaceName);

            // Generate SortingLayerName
            var sortingLayerCode = CodeGenerator.GenerateConstClass("SortingLayerName", NameGetter.GetSortingLayers().Select(n => n.Name).ToArray());
            Generate(outputPath, sortingLayerCode, "SortingLayerName", namespaceName);

            AssetDatabase.Refresh();
        }

        public static void GenerateNamesCodes(string outputPath, string namespaceName = null)
        {
            DeleteDirectoryIfExists(outputPath, true);
            CreateDirectoryIfNotExists(outputPath);

            // Generate TagName and Tags
            var tagNameCode = CodeGenerator.GenerateStruct("TagName", Property.StringName);
            Generate(outputPath, tagNameCode, "TagName", namespaceName);

            var tagsCode = CodeGenerator.GenerateParentStaticClass("Tags", "TagName", NameGetter.GetTags());
            Generate(outputPath, tagsCode, "Tags", namespaceName);

            // Generate LayerName and Layers
            var layerNameCode = CodeGenerator.GenerateStruct("LayerName", Property.StringName, Property.IntIndex);
            Generate(outputPath, layerNameCode, "LayerName", namespaceName);

            var layersCode = CodeGenerator.GenerateParentStaticClass("Layers", "LayerName", NameGetter.GetLayers().ToArray());
            Generate(outputPath, layersCode, "Layers", namespaceName);

            // Generate SceneName and Scenes
            var sceneNameCode = CodeGenerator.GenerateStruct("SceneName", Property.StringName);
            Generate(outputPath, sceneNameCode, "SceneName", namespaceName);

            var scenesCode = CodeGenerator.GenerateParentStaticClass("Scenes", "SceneName", NameGetter.GetScenes().Select(s => s.Name).ToArray());
            Generate(outputPath, scenesCode, "Scenes", namespaceName);

            // Generate SortingLayerName and SortingLayer
            var sortingLayerNameCode = CodeGenerator.GenerateStruct("SortingLayerName", Property.StringName, Property.IntId);
            Generate(outputPath, sortingLayerNameCode, "SortingLayerName", namespaceName);

            var sortingLayersCode = CodeGenerator.GenerateParentStaticClass("SortingLayers", "SortingLayerName", NameGetter.GetSortingLayers().ToArray());
            Generate(outputPath, sortingLayersCode, "SortingLayers", namespaceName);

            AssetDatabase.Refresh();
        }

        static void Generate(string outputPath, string code, string typeName, string namespaceName)
        {
            if (namespaceName != null)
            {
                var dirPath = Path.Combine(outputPath, namespaceName.Replace('.', '/'));
                CreateDirectoryIfNotExists(dirPath);
                var outPath = Path.Combine(dirPath, typeName + ".cs");
                File.WriteAllText(outPath, CodeGenerator.AddNamespace(namespaceName, code));
            }
            else
            {
                var outPath = Path.Combine(outputPath, typeName + ".cs");
                File.WriteAllText(outPath, code);
            }
        }

        static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        static void DeleteDirectoryIfExists(string path, bool recursive = false)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive);
        }
    }
}
