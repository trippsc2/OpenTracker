using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Interfaces;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels
{
    public class PinnedLocationControlVM : ViewModelBase, IPinnedLocationControlVM
    {
        private readonly MainWindowVM _mainWindow;

        public Location Location { get; }
        public string Name { get; }
        public ObservableCollection<SectionControlVM> Sections { get; }

        public PinnedLocationControlVM(AppSettingsVM appSettings, Game game, MainWindowVM mainWindow, Location location)
        {
            _mainWindow = mainWindow;

            Location = location;
            Name = location.Name;

            Sections = new ObservableCollection<SectionControlVM>();

            foreach (ISection section in location.Sections)
                Sections.Add(new SectionControlVM(appSettings, game, section));
        }

        public void Close()
        {
            _mainWindow.PinnedLocations.Remove(this);
        }
    }
}