using Raylib_cs;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TetrisCs;
public static class ResourceLoader
{
    public static (string, byte[]) GetMemoryLoader(string fileName)
    {
        string extention = Path.GetExtension(fileName);

        string resouceName = $"{nameof(TetrisCs)}.{fileName.Replace("/",".")}";
        byte[] bytes = GetResourceBytes(resouceName);
        return (extention, bytes);
    }

    private static byte[] GetResourceBytes(string resourceName)
    {
        using (var stream = GetResourceStream(resourceName))
        {
            if (stream == null)
                throw new FileNotFoundException("Resource not found: " + resourceName);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
    private static Stream? GetResourceStream(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return assembly.GetManifestResourceStream(resourceName);
    }
}
