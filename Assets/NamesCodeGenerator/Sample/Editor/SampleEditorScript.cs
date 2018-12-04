using UnityEditor;

namespace NamesCodeGenerator.Sample
{
    public static class SampleEditorScript
    {
        [MenuItem("Assets/Names Code Generator/Generate %g")]
        static void GenerateCode()
        {
            var outputPath = "Assets/Generated";
            NamesCodeGenerator.GenerateNamesCodes(outputPath, "Names", true);
        }

        [MenuItem("Assets/Names Code Generator/Generate Simple")]
        static void GenerateSimpleCode()
        {
            var outputPath = "Assets/Generated";
            NamesCodeGenerator.GenerateConstStaticClasses(outputPath, "Names.Simple");
        }

        [MenuItem("Assets/Names Code Generator/Generate %g", true)]
        [MenuItem("Assets/Names Code Generator/Generate Simple", true)]
        static bool Validate()
        {
            return !EditorApplication.isCompiling && !EditorApplication.isPlaying;
        }
    }
}