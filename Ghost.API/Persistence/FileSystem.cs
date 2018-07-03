using System.IO;

namespace Ghost.API.Persistence
{
    public class FileSystem : IFileSystem
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
