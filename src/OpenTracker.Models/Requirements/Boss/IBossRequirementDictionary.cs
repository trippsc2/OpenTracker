using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.Requirements.Boss
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IBossRequirement"/>
    /// objects indexed by <see cref="BossPlacementID"/>.
    /// </summary>
    public interface IBossRequirementDictionary : IDictionary<BossPlacementID, IRequirement>
    {
    }
}