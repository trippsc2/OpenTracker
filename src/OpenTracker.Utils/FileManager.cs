using System.IO;

namespace OpenTracker.Utils
{
    /// <summary>
    /// This class contains the logic for manipulating the file system, abstracted to allow for non-destructive unit
    /// tests.
    /// </summary>
    public class FileManager : IFileManager
    {
        public void EnsureFileDoesNotExist(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}