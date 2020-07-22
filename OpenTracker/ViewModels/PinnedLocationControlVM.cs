using OpenTracker.Interfaces;
using OpenTracker.Models.Locations;
using OpenTracker.Models.UndoRedo;
using OpenTracker.ViewModels.SectionControls;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the pinned location control.
    /// </summary>
    public class PinnedLocationControlVM : ViewModelBase, IClickHandler
    {
        private readonly ObservableCollection<PinnedLocationControlVM> _pinnedLocations;

        public string Name =>
            Location.Name;
        public ILocation Location { get; }
        public ObservableCollection<SectionControlVM> Sections { get; } =
            new ObservableCollection<SectionControlVM>();

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
        public PinnedLocationControlVM(
            ILocation location, ObservableCollection<PinnedLocationControlVM> pinnedLocations,
            ObservableCollection<SectionControlVM> sections)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
            _pinnedLocations = pinnedLocations ??
                throw new ArgumentNullException(nameof(pinnedLocations));
            Sections = sections ?? throw new ArgumentNullException(nameof(sections));
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