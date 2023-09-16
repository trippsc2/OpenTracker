using System.Collections.Generic;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.Locations;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> of <see cref="ILocation"/> objects indexed
/// by <see cref="LocationID"/>.
/// </summary>
public interface ILocationDictionary : IDictionary<LocationID, ILocation>, IResettable,
    ISaveable<IDictionary<LocationID, LocationSaveData>>
{
}