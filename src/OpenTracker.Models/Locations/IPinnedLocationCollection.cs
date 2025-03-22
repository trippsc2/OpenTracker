using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;

namespace OpenTracker.Models.Locations
{
    /// <summary>
    /// This interface contains the <see cref="ObservableCollection{T}"/> container for pinned location data.
    /// </summary>
    public interface IPinnedLocationCollection : IObservableCollection<ILocation>, ISaveable<IList<LocationID>>
    {
    }
}