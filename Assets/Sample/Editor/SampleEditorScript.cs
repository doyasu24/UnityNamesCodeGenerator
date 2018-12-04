using UnityEditor;

namespace NamesCode.Generator.Sample
{
    public static class SampleEditorScript
    {
        [MenuItem("Assets/Names Code Generator/Generate %g")]
        private static void GenerateCode()
        {
            const string outputPath = "Assets/Generated";
            NamesCodeGenerator.GenerateNamesCodes(outputPath);
        }

        [MenuItem("Assets/Names Code Generator/Generate %g", true)]
        private static bool Validate()
        {
            return !EditorApplication.isCompiling && !EditorApplication.isPlaying;
        }
    }
}