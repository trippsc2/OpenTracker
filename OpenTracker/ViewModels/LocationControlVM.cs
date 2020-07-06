using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Undoables;
using OpenTracker.Models.Sections;
using System;
using System.Collections.ObjectModel;
using OpenTracker.ViewModels.Bases;
using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the view-model for the pinned location controls.
    /// </summary>
    public class LocationControlVM : ViewModelBase, IClose
    {
        private readonly UndoRedoManager _undoRedoManager;
        private readonly MainWindowVM _mainWindow;

        public string Name =>
            Location.Name;
        public ILocation Location { get; }
        public ObservableCollection<SectionControlVM> Sections { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="appSettings">
        /// The app settings.
        /// </param>
        /// <param name="game">
        /// The game data.
        /// </param>
        /// <param name="mainWindow">
        /// The view-model of the main window.
        /// </param>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        public LocationControlVM(UndoRedoManager undoRedoManager, AppSettings appSettings,
            MainWindowVM mainWindow, ILocation location)
        {
            _undoRedoManager = undoRedoManager;
            _mainWindow = mainWindow;

            Location = location ?? throw new ArgumentNullException(nameof(location));

            Sections = new ObservableCollection<SectionControlVM>();

            foreach (ISection section in location.Sections)
            {
                Sections.Add(new SectionControlVM(undoRedoManager, appSettings, section));
            }
        }

        /// <summary>
        /// Unpins the location.
        /// </summary>
        public void Close()
        {
            _undoRedoManager.Execute(new UnpinLocation(_mainWindow.Locations, this));
        }
    }
}