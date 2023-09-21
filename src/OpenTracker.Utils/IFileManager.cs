namespace OpenTracker.Utils;

/// <summary>
/// This interface contains the logic for manipulating the file system, abstracted to allow for non-destructive unit
/// tests.
/// </summary>
public interface IFileManager
{
    /// <summary>
    /// Ensures that the file path specified does not contain an existing file by deleting it if it exists.
    /// </summary>
    /// <param name="path">
    ///     A <see cref="string"/> representing the file path.
    /// </param>
    void EnsureFileDoesNotExist(string path);
}