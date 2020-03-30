namespace OpenTracker.Interfaces
{
    public interface IMainWindowVM
    {
        void SaveAppSettings();
        void Save(string path);
        void Open(string path);
    }
}
