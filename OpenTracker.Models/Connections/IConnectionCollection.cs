using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    /// This is the interface for the collection container of map connections between map locations.
    /// </summary>
    public interface IConnectionCollection : IObservableCollection<IConnection>,
        ISaveable<List<ConnectionSaveData>>
    {
    }
}
