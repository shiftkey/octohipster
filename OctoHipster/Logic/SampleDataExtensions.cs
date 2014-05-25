using System.IO;
using System.Reflection;

namespace OctoHipster.Logic
{
    public static class SampleDataExtensions
    {
        public static string GetFileContents(this Assembly assembly, string fileName)
        {
            var currentDirectory = Path.GetDirectoryName(assembly.Location);
            var dataFile = Path.Combine(currentDirectory, "data", fileName);
            return File.ReadAllText(dataFile);
        }
    }
}
