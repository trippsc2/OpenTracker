using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This interface contains the dictionary container for boss placements.
    /// </summary>
    public interface IBossPlacementDictionary : IDictionary<BossPlacementID, IBossPlacement>,
        ISaveable<Dictionary<BossPlacementID, BossPlacementSaveData>>
    {
        void Reset();
    }
}