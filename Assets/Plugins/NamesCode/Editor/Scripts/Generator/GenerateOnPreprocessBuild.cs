using System;
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
            try
            {
                NamesCodeGenerator.Generate();
            }
            catch (Exception e)
            {
                throw new BuildFailedException(e); // to fail to build
            }
        }
    }
}