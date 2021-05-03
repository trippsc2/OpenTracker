using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.BossShuffle
{
    /// <summary>
    ///     This interface contains the dictionary container for boss shuffle requirements.
    /// </summary>
    public interface IBossShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}