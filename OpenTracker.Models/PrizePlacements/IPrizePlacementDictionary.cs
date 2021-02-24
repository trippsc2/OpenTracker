using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;

namespace OpenTracker.Models.PrizePlacements
{
    public interface IPrizePlacementDictionary : IDictionary<PrizePlacementID, IPrizePlacement>,
        ICollection<KeyValuePair<PrizePlacementID, IPrizePlacement>>,
        ISaveable<Dictionary<PrizePlacementID, PrizePlacementSaveData>>
    {
        void Reset();
    }
}