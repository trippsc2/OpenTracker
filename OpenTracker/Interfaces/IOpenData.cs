namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface for opening and loading a file.
    /// </summary>
    public interface IOpenData
    {
        string CurrentFilePath { get; }

        void Open(string path);
    }
}
