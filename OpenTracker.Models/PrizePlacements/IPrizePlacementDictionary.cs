using System.Collections.Generic;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.Models.PrizePlacements
{
    /// <summary>
    ///     This interface contains the dictionary container for prize placement data.
    /// </summary>
    public interface IPrizePlacementDictionary : IDictionary<PrizePlacementID, IPrizePlacement>,
        ISaveable<IDictionary<PrizePlacementID, PrizePlacementSaveData>>
    {
        /// <summary>
        ///     Resets the contained boss placements to their starting values.
        /// </summary>
        void Reset();
    }
}