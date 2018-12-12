using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NamesCode.Drawers
{
    [CustomPropertyDrawer(typeof(SceneName))]
    public class SceneNameDrawer : PropertyDrawer
    {
        private static readonly string[] SceneNameArray = Scenes.Names.Select(s => s.Name).ToArray();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Scenes.Names.Length == 0)
            {
                EditorGUI.LabelField(position, ObjectNames.NicifyVariableName(property.name), "Scene is Empty");
                return;
            }

            var nameProperty = property.FindPropertyRelative("_name");
            var indexProperty = property.FindPropertyRelative("_index");

            if (nameProperty == null)
            {
                Debug.LogWarning("Scene name property null");
                return;
            }

            if (indexProperty == null)
            {
                Debug.LogWarning("Scene index property null");
                return;
            }
            
            var currentIndex = Scenes.Names
                .Where(s => s.Name == nameProperty.stringValue)
                .Select(s => s.Index)
                .FirstOrDefault();

            var nextIndex = EditorGUI.Popup(position, label.text, currentIndex, SceneNameArray);

            foreach (var s in Scenes.Names)
            {
                if (s.Index != nextIndex) continue;
                indexProperty.intValue = s.Index;
                nameProperty.stringValue = s.Name;
                break;
            }
        }
    }
}