using System.Collections.Generic;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IPrizePlacement"/>
    /// objects indexed by <see cref="PrizePlacementID"/>.
    /// </summary>
    public interface IPrizePlacementDictionary : IDictionary<PrizePlacementID, IPrizePlacement>, IResettable,
        ISaveable<IDictionary<PrizePlacementID, PrizePlacementSaveData>>
    {
    }
}