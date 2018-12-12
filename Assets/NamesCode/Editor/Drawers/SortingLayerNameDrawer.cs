using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NamesCode.Drawers
{
    [CustomPropertyDrawer(typeof(SortingLayerName))]
    public class SortingLayerNameDrawer : PropertyDrawer
    {
        private static readonly string[] SortingLayerNameArray = SortingLayers.Names.Select(s => s.Name).ToArray();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (SortingLayers.Names.Length == 0)
            {
                EditorGUI.LabelField(position, ObjectNames.NicifyVariableName(property.name), "SortingLayer is Empty");
                return;
            }

            var nameProperty = property.FindPropertyRelative("_name");
            var idProperty = property.FindPropertyRelative("_id");

            if (nameProperty == null)
            {
                Debug.LogWarning("SortingLayer name property null");
                return;
            }

            if (idProperty == null)
            {
                Debug.LogWarning("SortingLayer id property null");
                return;
            }
            
            var currentId = SortingLayers.Names
                .Where(s => s.Name == nameProperty.stringValue)
                .Select(s => s.Id)
                .FirstOrDefault();

            var nextId = EditorGUI.Popup(position, label.text, currentId, SortingLayerNameArray);

            foreach (var s in SortingLayers.Names)
            {
                if (s.Id != nextId) continue;
                idProperty.intValue = s.Id;
                nameProperty.stringValue = s.Name;
                break;
            }
        }
    }
}