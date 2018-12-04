using UnityEditor;

namespace NamesCode.Generator.Sample
{
    public static class SampleEditorScript
    {
        [MenuItem("Assets/Names Code Generator/Generate %g")]
        static void GenerateCode()
        {
            var outputPath = "Assets/Generated";
            NamesCodeGenerator.GenerateNamesCodes(outputPath);
        }

        [MenuItem("Assets/Names Code Generator/Generate %g", true)]
        static bool Validate()
        {
            return !EditorApplication.isCompiling && !EditorApplication.isPlaying;
        }
    }
}