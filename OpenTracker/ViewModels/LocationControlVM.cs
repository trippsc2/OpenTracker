using OpenTracker.Actions;
using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Interfaces;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels
{
    public class LocationControlVM : ViewModelBase, ILocationControlVM
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly MainWindowVM _mainWindow;

        public Location Location { get; }
        public string Name => Location.Name;
        public ObservableCollection<SectionControlVM> Sections { get; }

        public LocationControlVM(UndoRedoManager undoRedoManager, AppSettingsVM appSettings,
            Game game, MainWindowVM mainWindow, Location location)
        {
            _undoRedoManager = undoRedoManager;
            _mainWindow = mainWindow;

            Location = location;

            Sections = new ObservableCollection<SectionControlVM>();

            foreach (ISection section in location.Sections)
                Sections.Add(new SectionControlVM(undoRedoManager, appSettings, game, section));
        }

        public void Close()
        {
            _undoRedoManager.Execute(new UnpinLocation(_mainWindow.Locations, this));
        }
    }
}