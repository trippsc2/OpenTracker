using OpenTracker.Models.Locations;
using OpenTracker.ViewModels.SectionControls;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the class for creating pinned location control ViewModel classes.
    /// </summary>
    public static class LocationControlVMFactory
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
        private static ObservableCollection<SectionControlVM> GetLocationControlVMSections(
            ILocation location)
        {
            ObservableCollection<SectionControlVM> sections = new ObservableCollection<SectionControlVM>();

            foreach (var section in location.Sections)
            {
                sections.Add(SectionControlVMFactory.GetSectionControlVM(section));
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
        public static PinnedLocationControlVM GetLocationControlVM(
            ILocation location, ObservableCollection<PinnedLocationControlVM> pinnedLocations)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            if (pinnedLocations == null)
            {
                throw new ArgumentNullException(nameof(pinnedLocations));
            }

            return new PinnedLocationControlVM(location, pinnedLocations, GetLocationControlVMSections(location));
        }
    }
}
