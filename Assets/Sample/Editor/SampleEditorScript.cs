using System.IO;
using NamesCode.Settings;
using UnityEditor;

namespace NamesCode.Generator.Sample
{
    public static class SampleEditorScript
    {
        [MenuItem("Assets/Names Code Generator/Generate %g")]
        private static void GenerateCode()
        {
            var setting = AssetDatabase.LoadAssetAtPath<GeneratorSetting>(GeneratorSetting.Path);
            if (setting == null) throw new FileNotFoundException("GeneratorSetting not found: " + GeneratorSetting.Path);
            NamesCodeGenerator.GenerateNamesCodes(setting.OutputDirectory);
        }
    }
}