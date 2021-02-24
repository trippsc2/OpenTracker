using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using System.Collections.Generic;

namespace OpenTracker.Models.Locations
{
    public interface IPinnedLocationCollection : IObservableCollection<ILocation>,
        ISaveable<List<LocationID>>
    {
    }
}