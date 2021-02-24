using OpenTracker.Models.Markings;
using System.Collections.ObjectModel;

namespace OpenTracker.Models.Locations
{
    public class LocationNoteCollection : ObservableCollection<IMarking>,
        ILocationNoteCollection
    {
    }
}
