using System.IO;
using System.Linq;

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
            DeleteAllFilesWithoutMetaFiles(directoryPath);
            CreateDirectoryIfNotExists(directoryPath);
        }

        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private static void DeleteAllFilesWithoutMetaFiles(string path)
        {
            if (!Directory.Exists(path)) return;
            
            foreach (var d in Directory.GetDirectories(path))
                DeleteAllFilesWithoutMetaFiles(d);

            var notMetaFiles = Directory.GetFiles(path).Where(f => !f.EndsWith(".meta"));
            foreach (var f in notMetaFiles)
                File.Delete(f);

            if (IsDirectoryEmpty(path))
                Directory.Delete(path);
        }

        private static bool IsDirectoryEmpty(string directoryPath)
        {
            return Directory.GetDirectories(directoryPath).Length <= 0 &&
                   Directory.GetFiles(directoryPath).Length <= 0;
        }
    }
}