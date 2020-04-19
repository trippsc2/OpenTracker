using OpenTracker.ViewModels;

namespace OpenTracker.Interfaces
{
    public interface IMainWindowVM
    {
        IAppSettingsVM AppSettingsInterface { get; }
        IAutoTrackerDialogVM AutoTrackerInterface { get; }

        void SaveAppSettings();
        void Save(string path);
        void Open(string path);
    }
}
