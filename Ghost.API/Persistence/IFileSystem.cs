namespace Ghost.API.Persistence
{
    public interface IFileSystem
    {
        string ReadAllText(string path);
    }
}
