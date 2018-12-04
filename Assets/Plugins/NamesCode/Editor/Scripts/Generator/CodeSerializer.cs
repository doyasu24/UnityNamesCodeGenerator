using System.IO;

namespace NamesCode.Generator
{
    public static class CodeSerializer
    {
        public static void WriteCodeFile(string outputPath, string code, string typeName, string namespaceName)
        {
            string outPath;
            if (namespaceName != null)
            {
                var dirPath = Path.Combine(outputPath, namespaceName.Replace('.', '/'));
                CreateDirectoryIfNotExists(dirPath);
                outPath = Path.Combine(dirPath, typeName + ".cs");
            }
            else
            {
                outPath = Path.Combine(outputPath, typeName + ".cs");
            }
            File.WriteAllText(outPath, code);
        }

        public static void ResetDirectory(string directoryPath)
        {
            DeleteDirectoryIfExists(directoryPath, true);
            CreateDirectoryIfNotExists(directoryPath);
        }

        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private static void DeleteDirectoryIfExists(string path, bool recursive = false)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, recursive);
        }
    }
}