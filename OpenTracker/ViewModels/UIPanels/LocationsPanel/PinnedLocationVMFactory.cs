using OpenTracker.Models.Locations;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.UIPanels.LocationsPanel
{
    /// <summary>
    /// This is the class for creating pinned location control ViewModel classes.
    /// </summary>
    public static class PinnedLocationVMFactory
    {
        /// <summary>
        /// Returns an observable collection of section control ViewModel instances for the
        /// specified location.
        /// </summary>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        /// <returns>
        /// An observable collection of section control ViewModel instances.
        /// </returns>
        private static ObservableCollection<SectionVM> GetPinnedLocationVMSections(
            ILocation location)
        {
            ObservableCollection<SectionVM> sections = new ObservableCollection<SectionVM>();

            foreach (var section in location.Sections)
            {
                sections.Add(SectionVMFactory.GetSectionVM(section));
            }

            return sections;
        }

        /// <summary>
        /// Returns a new location control ViewModel instance.
        /// </summary>
        /// <param name="location">
        /// The location to be represented.
        /// </param>
        /// <param name="pinnedLocations">
        /// The observable collection of pinned location control ViewModel instances.
        /// </param>
        /// <returns>
        /// A new location control ViewModel instance.
        /// </returns>
        public static PinnedLocationVM GetLocationControlVM(
            ILocation location, ObservableCollection<PinnedLocationVM> pinnedLocations)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            if (pinnedLocations == null)
            {
                throw new ArgumentNullException(nameof(pinnedLocations));
            }

            return new PinnedLocationVM(location, pinnedLocations, GetPinnedLocationVMSections(location));
        }
    }
}
