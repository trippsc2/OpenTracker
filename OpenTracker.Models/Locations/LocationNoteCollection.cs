using OpenTracker.Models.Markings;
using System.Collections.ObjectModel;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This class contains the collection container of location notes.
    /// </summary>
    public class LocationNoteCollection : ObservableCollection<IMarking>,
        ILocationNoteCollection
    {
    }
}
