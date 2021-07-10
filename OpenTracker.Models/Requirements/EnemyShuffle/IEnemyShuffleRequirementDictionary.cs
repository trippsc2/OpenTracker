using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.EnemyShuffle
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="IEnemyShuffleRequirement"/> objects indexed by <see cref="bool"/>.
    /// </summary>
    public interface IEnemyShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}