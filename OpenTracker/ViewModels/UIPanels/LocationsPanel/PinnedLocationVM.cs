using OpenTracker.Interfaces;
using OpenTracker.Models;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OpenTracker.ViewModels.UIPanels.LocationsPanel
{
    /// <summary>
    /// This is the ViewModel for the pinned location control.
    /// </summary>
    public class PinnedLocationVM : ViewModelBase, IClickHandler
    {
        private readonly ILocation _location;
        private readonly ObservableCollection<PinnedLocationVM> _pinnedLocations;

        public double Scale =>
            AppSettings.Instance.UIScale;
        public string Name =>
            _location.Name;
        public ObservableCollection<SectionVM> Sections { get; } =
            new ObservableCollection<SectionVM>();
        public PinnedLocationNoteAreaVM Notes { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        /// <param name="pinnedLocations">
        /// The observable collection of pinned location control ViewModels.
        /// </param>
        /// <param name="sections">
        /// The observable collection of section control ViewModels.
        /// </param>
        public PinnedLocationVM(
            ILocation location, ObservableCollection<PinnedLocationVM> pinnedLocations,
            ObservableCollection<SectionVM> sections)
        {
            _location = location ?? throw new ArgumentNullException(nameof(location));
            _pinnedLocations = pinnedLocations ??
                throw new ArgumentNullException(nameof(pinnedLocations));
            Sections = sections ?? throw new ArgumentNullException(nameof(sections));
            Notes = new PinnedLocationNoteAreaVM(_location);

            AppSettings.Instance.PropertyChanged += OnAppSettingsChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the AppSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnAppSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AppSettings.UIScale))
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
            UndoRedoManager.Instance.Execute(new UnpinLocation(_pinnedLocations, this));
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