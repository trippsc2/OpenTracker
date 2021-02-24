using OpenTracker.Interfaces;
using OpenTracker.Models.Locations;
using OpenTracker.Utils;
using OpenTracker.ViewModels.PinnedLocations.Notes;
using OpenTracker.ViewModels.PinnedLocations.Sections;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.PinnedLocations
{
    public interface IPinnedLocationVM : IModelWrapper, IClickHandler
    {
        delegate IPinnedLocationVM Factory(
            ILocation location, List<ISectionVM> sections, IPinnedLocationNoteAreaVM notes);
    }
}