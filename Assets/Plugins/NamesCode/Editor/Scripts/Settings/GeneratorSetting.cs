using UnityEngine;

namespace NamesCode.Settings
{
    [CreateAssetMenu(
        menuName = "NamesCode/GeneratorSetting",
        fileName = Path
    )]
    public class GeneratorSetting : ScriptableObject
    {
        public string OutputDirectory;

        public const string Path = "Assets/Plugins/NamesCode/Editor/GeneratorSetting.asset";
    }
}