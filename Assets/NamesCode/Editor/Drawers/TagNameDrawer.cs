using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NamesCode.Drawers
{
    [CustomPropertyDrawer(typeof(TagName))]
    public class TagNameDrawer : PropertyDrawer
    {
        private static readonly string[] TagNameArray = Tags.Names.Select(s => s.Name).ToArray();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Tags.Names.Length == 0)
            {
                EditorGUI.LabelField(position, ObjectNames.NicifyVariableName(property.name), "Tag is Empty");
                return;
            }

            var nameProperty = property.FindPropertyRelative("_name");
            var indexProperty = property.FindPropertyRelative("_index");

            if (nameProperty == null)
            {
                Debug.LogWarning("Tag name property null");
                return;
            }

            if (indexProperty == null)
            {
                Debug.LogWarning("Tag index property null");
                return;
            }

            var currentIndex = Tags.Names
                .Where(s => s.Name == nameProperty.stringValue)
                .Select(s => s.Index)
                .FirstOrDefault();

            var nextIndex = EditorGUI.Popup(position, label.text, currentIndex, TagNameArray);

            foreach (var s in Tags.Names)
            {
                if (s.Index != nextIndex) continue;
                indexProperty.intValue = s.Index;
                nameProperty.stringValue = s.Name;
                break;
            }
        }
    }
}