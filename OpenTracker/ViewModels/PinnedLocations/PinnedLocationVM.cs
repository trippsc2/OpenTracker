using OpenTracker.Models.Locations;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.PinnedLocations.Notes;
using OpenTracker.ViewModels.PinnedLocations.Sections;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This is the ViewModel for the pinned location control.
    /// </summary>
    public class PinnedLocationVM : ViewModelBase, IPinnedLocationVM
    {
        private readonly ILayoutSettings _layoutSettings;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly ILocation _location;

        public double Scale =>
            _layoutSettings.UIScale;
        public string Name =>
            _location.Name;
        public object Model =>
            _location;

        public List<ISectionVM> Sections { get; }
        public IPinnedLocationNoteAreaVM Notes { get; }

        public delegate IPinnedLocationVM Factory(
            ILocation location, List<ISectionVM> sections, IPinnedLocationNoteAreaVM notes);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        /// <param name="sections">
        /// The observable collection of section control ViewModels.
        /// </param>
        public PinnedLocationVM(
            ILayoutSettings layoutSettings, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory, ILocation location, List<ISectionVM> sections,
            IPinnedLocationNoteAreaVM notes)
        {
            _layoutSettings = layoutSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _location = location;

            Sections = sections;
            Notes = notes;

            _layoutSettings.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutSettings.UIScale))
            {
                this.RaisePropertyChanged(nameof(Scale));
            }
        }

        /// <summary>
        /// Handles left clicks and removes the pinned location.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            _undoRedoManager.Execute(_undoableFactory.GetUnpinLocation(_location));
        }

        /// <summary>
        /// Handles right clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
        }
    }
}