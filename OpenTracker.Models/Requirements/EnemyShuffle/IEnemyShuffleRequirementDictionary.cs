using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.EnemyShuffle
{
    /// <summary>
    ///     This interface contains the dictionary container for enemy shuffle requirements.
    /// </summary>
    public interface IEnemyShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}