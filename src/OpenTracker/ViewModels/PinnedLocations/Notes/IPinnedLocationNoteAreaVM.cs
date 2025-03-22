using OpenTracker.Models.Locations;

namespace OpenTracker.ViewModels.PinnedLocations.Notes
{
    public interface IPinnedLocationNoteAreaVM
    {
        delegate IPinnedLocationNoteAreaVM Factory(ILocation location);
    }
}