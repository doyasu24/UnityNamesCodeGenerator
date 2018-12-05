using System.IO;

namespace NamesCode.Generator
{
    public static class CodeSerializer
    {
        public static void WriteCodeFile(string outputDirectory, string code, string typeName)
        {
            CreateDirectoryIfNotExists(outputDirectory);
            var path = Path.Combine(outputDirectory, typeName + ".cs");
            File.WriteAllText(path, code);
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