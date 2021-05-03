using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.BigKeyShuffle
{
    /// <summary>
    ///     This interface contains the dictionary container for big key shuffle requirements.
    /// </summary>
    public interface IBigKeyShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}