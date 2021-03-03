using Avalonia.Input;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Settings;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using OpenTracker.ViewModels.PinnedLocations.Notes;
using OpenTracker.ViewModels.PinnedLocations.Sections;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This class contains the pinned location control ViewModel data.
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

        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="layoutSettings">
        /// The layout settings data.
        /// </param>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// A factory for creating undoable actions.
        /// </param>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        /// <param name="sections">
        /// The observable collection of section control ViewModels.
        /// </param>
        /// <param name="notes">
        /// The pinned location note area control.
        /// </param>
        public PinnedLocationVM(
            ILayoutSettings layoutSettings, IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory,
            ILocation location, List<ISectionVM> sections, IPinnedLocationNoteAreaVM notes)
        {
            _layoutSettings = layoutSettings;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _location = location;

            Sections = sections;
            Notes = notes;
            
            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

            _layoutSettings.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ILayoutSettings interface.
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
        /// Creates an undoable action to remove the pinned location and sends it to undo/redo manager.
        /// </summary>
        private void UnpinLocation()
        {
            _undoRedoManager.Execute(_undoableFactory.GetUnpinLocation(_location));
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The PointerReleased event args.
        /// </param>
        private void HandleClick(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                UnpinLocation();
            }
        }
    }
}