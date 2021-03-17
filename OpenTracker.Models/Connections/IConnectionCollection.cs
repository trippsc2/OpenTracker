using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;

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
