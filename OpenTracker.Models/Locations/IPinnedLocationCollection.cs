using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains the collection container for pinned location data.
    /// </summary>
    public interface IPinnedLocationCollection : IObservableCollection<ILocation>,
        ISaveable<List<LocationID>>
    {
    }
}