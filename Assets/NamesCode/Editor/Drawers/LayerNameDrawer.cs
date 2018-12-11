using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NamesCode.Drawers
{
    [CustomPropertyDrawer(typeof(LayerName))]
    public class LayerNameDrawer : PropertyDrawer
    {
        private static readonly string[] LayerNameArray = Layers.Names.Select(s => s.Name).ToArray();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Layers.Names.Length == 0)
            {
                EditorGUI.LabelField(position, ObjectNames.NicifyVariableName(property.name), "Layer is Empty");
                return;
            }

            var nameProperty = property.FindPropertyRelative("_name");
            var indexProperty = property.FindPropertyRelative("_index");

            if (nameProperty == null)
            {
                Debug.LogWarning("Layer name property null");
                return;
            }

            if (indexProperty == null)
            {
                Debug.LogWarning("Layer index property null");
                return;
            }
            
            var currentIndex = Layers.Names
                .Where(s => s.Name == nameProperty.stringValue)
                .Select(s => s.Index)
                .FirstOrDefault();

            var nextIndex = EditorGUI.Popup(position, label.text, currentIndex, LayerNameArray);

            foreach (var s in Layers.Names)
            {
                if (s.Index != nextIndex) continue;
                indexProperty.intValue = s.Index;
                nameProperty.stringValue = s.Name;
                break;
            }
        }
    }
}