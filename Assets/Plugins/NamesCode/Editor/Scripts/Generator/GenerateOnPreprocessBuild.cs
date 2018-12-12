using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace NamesCode.Generator
{
    public class GenerateOnPreprocessBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder
        {
            get { return 0; }
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            NamesCodeGenerator.Generate();
        }
    }
}