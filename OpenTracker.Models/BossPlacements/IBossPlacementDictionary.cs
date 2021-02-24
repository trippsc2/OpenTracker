using OpenTracker.Models.SaveLoad;
using System.Collections.Generic;

namespace OpenTracker.Models.BossPlacements
{
    public interface IBossPlacementDictionary : IDictionary<BossPlacementID, IBossPlacement>,
        ICollection<KeyValuePair<BossPlacementID, IBossPlacement>>,
        ISaveable<Dictionary<BossPlacementID, BossPlacementSaveData>>
    {
        void Reset();
    }
}