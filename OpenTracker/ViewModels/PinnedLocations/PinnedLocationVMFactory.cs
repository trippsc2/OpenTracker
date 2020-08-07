using OpenTracker.Models.Locations;
using System;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.PinnedLocations
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
        /// <returns>
        /// A new location control ViewModel instance.
        /// </returns>
        public static PinnedLocationVM GetLocationControlVM(ILocation location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            return new PinnedLocationVM(location, GetPinnedLocationVMSections(location));
        }
    }
}
