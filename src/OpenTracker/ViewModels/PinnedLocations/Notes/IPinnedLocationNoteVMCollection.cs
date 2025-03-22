using OpenTracker.Models.Locations;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.PinnedLocations.Notes
{
    public interface IPinnedLocationNoteVMCollection : IObservableCollection<IPinnedLocationNoteVM>
    {
        public delegate IPinnedLocationNoteVMCollection Factory(ILocation location);
    }
}