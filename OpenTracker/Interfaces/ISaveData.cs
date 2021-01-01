namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface to save the tracker data.
    /// </summary>
    public interface ISaveData
    {
        string CurrentFilePath { get; }

        void Save(string path = null);
    }
}
