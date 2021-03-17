using System.Collections.ObjectModel;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This class contains the collection container of location notes.
    /// </summary>
    public class LocationNoteCollection : ObservableCollection<IMarking>, ILocationNoteCollection
    {
    }
}
