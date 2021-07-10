using System.Collections.Generic;
using OpenTracker.Models.Reset;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This interface contains the dictionary container for <see cref="IBossPlacement"/> objects.
    /// </summary>
    public interface IBossPlacementDictionary : IDictionary<BossPlacementID, IBossPlacement>, IResettable,
        ISaveable<IDictionary<BossPlacementID, BossPlacementSaveData>>
    {
    }
}