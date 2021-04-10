using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    ///     This interface contains the dictionary container for boss requirements.
    /// </summary>
    public interface IBossRequirementDictionary : IDictionary<BossPlacementID, IRequirement>
    {
    }
}