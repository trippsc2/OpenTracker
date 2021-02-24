using OpenTracker.Models.Markings;
using OpenTracker.Utils;

namespace OpenTracker.Models.Locations
{
    public interface ILocationNoteCollection : IObservableCollection<IMarking>
    {
    }
}