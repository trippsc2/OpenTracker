using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    /// This interface contains the dictionary container for prize placement data.
    /// </summary>
    public interface IPrizePlacementDictionary : IDictionary<PrizePlacementID, IPrizePlacement>,
        ISaveable<Dictionary<PrizePlacementID, PrizePlacementSaveData>>
    {
        void Reset();
    }
}