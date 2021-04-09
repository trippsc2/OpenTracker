using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.SmallKeyShuffle
{
    /// <summary>
    ///     This interface contains the dictionary container for small key shuffle requirements.
    /// </summary>
    public interface ISmallKeyShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}