using UnityEngine;
using UnityEngine.Serialization;

namespace NamesCode.Settings
{
    [CreateAssetMenu(menuName = "NamesCode/GeneratorSetting", fileName = "GeneratorSetting")]
    public class GeneratorSetting : ScriptableObject
    {
        [SerializeField] [FormerlySerializedAs("OutputDirectory")]
        private string outputDirectory = "Assets/NamesCode/Generated";

        public string OutputDirectory => outputDirectory;
    }
}