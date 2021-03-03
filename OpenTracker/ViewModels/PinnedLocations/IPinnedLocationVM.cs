using OpenTracker.Models.Locations;
using OpenTracker.Utils;
using OpenTracker.ViewModels.PinnedLocations.Notes;
using OpenTracker.ViewModels.PinnedLocations.Sections;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This interface contains the pinned location control ViewModel data.
    /// </summary>
    public interface IPinnedLocationVM : IModelWrapper
    {
        delegate IPinnedLocationVM Factory(
            ILocation location, List<ISectionVM> sections, IPinnedLocationNoteAreaVM notes);
    }
}