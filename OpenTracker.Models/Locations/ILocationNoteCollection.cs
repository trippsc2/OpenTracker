using OpenTracker.Models.Markings;
using OpenTracker.Utils;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains the collection container of location notes.
    /// </summary>
    public interface ILocationNoteCollection : IObservableCollection<IMarking>
    {
    }
}