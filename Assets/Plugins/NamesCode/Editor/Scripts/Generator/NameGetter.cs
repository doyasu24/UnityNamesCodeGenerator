using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace NamesCode.Generator
{
    public static class NameGetter
    {
        public static IEnumerable<NameWithNumber> GetTags()
        {
            var tags = InternalEditorUtility.tags;
            for (var i = 0; i < tags.Length; i++)
            {
                yield return new NameWithNumber(tags[i], i);
            }
        }

        public static IEnumerable<NameWithNumber> GetLayers()
        {
            return InternalEditorUtility.layers.Select(l => new NameWithNumber(l, LayerMask.NameToLayer(l)));
        }

        public static IEnumerable<NameWithNumber> GetScenes()
        {
            for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                var scene = EditorBuildSettings.scenes[i];
                var sceneName = Path.GetFileNameWithoutExtension(scene.path);
                yield return new NameWithNumber(sceneName, i);
            }
        }

        public static IEnumerable<NameWithNumber> GetSortingLayers()
        {
            var names = GetSortingLayerNames();
            var ids = GetSortingLayerUniqueIDs();
            for (var i = 0; i < names.Length; i++)
            {
                yield return new NameWithNumber(names[i], ids[i]);
            }
        }

        private static string[] GetSortingLayerNames()
        {
            var internalEditorUtilityType = typeof(InternalEditorUtility);
            var sortingLayersProperty =
                internalEditorUtilityType.GetProperty("sortingLayerNames",
                    BindingFlags.Static | BindingFlags.NonPublic);
            return (string[]) sortingLayersProperty.GetValue(null, new object[0]);
        }

        private static int[] GetSortingLayerUniqueIDs()
        {
            var internalEditorUtilityType = typeof(InternalEditorUtility);
            var sortingLayerUniqueIDsProperty = internalEditorUtilityType.GetProperty("sortingLayerUniqueIDs",
                BindingFlags.Static | BindingFlags.NonPublic);
            return (int[]) sortingLayerUniqueIDsProperty.GetValue(null, new object[0]);
        }
    }

    public struct NameWithNumber
    {
        public readonly string Name;
        public readonly int Number;

        public NameWithNumber(string name, int number)
        {
            Name = name;
            Number = number;
        }
    }
}