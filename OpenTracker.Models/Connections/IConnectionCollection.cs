using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    /// This interface contains the collection container for map connections.
    /// </summary>
    public interface IConnectionCollection : IObservableCollection<IConnection>,
        ISaveable<List<ConnectionSaveData>>
    {
    }
}
