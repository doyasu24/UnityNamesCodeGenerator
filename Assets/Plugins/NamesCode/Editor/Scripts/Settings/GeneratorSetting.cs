using UnityEngine;

namespace NamesCode.Settings
{
    [CreateAssetMenu(menuName = "NamesCode/GeneratorSetting")]
    public class GeneratorSetting : ScriptableObject
    {
        public string OutputDirectory;

        public static GeneratorSetting Default
        {
            get
            {
                var s = CreateInstance<GeneratorSetting>();
                s.OutputDirectory = "Assets/NamesCode/Generated/";
                return s;
            }
        }
    }
}