using System.IO;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Utils;

/// <summary>
/// This class contains the logic for manipulating the file system, abstracted to allow for non-destructive unit
/// tests.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class FileManager : IFileManager
{
    public void EnsureFileDoesNotExist(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}