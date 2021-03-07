using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the interface for the dictionary container for boss placements.
    /// </summary>
    public interface IBossPlacementDictionary : IDictionary<BossPlacementID, IBossPlacement>,
        ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>,
        ISaveable<Dictionary<BossPlacementID, BossPlacementSaveData>>
    {
        void Reset();
    }
}