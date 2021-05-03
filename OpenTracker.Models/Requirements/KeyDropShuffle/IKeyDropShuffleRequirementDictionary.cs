using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.KeyDropShuffle
{
    /// <summary>
    ///     This interface contains the dictionary container for key drop shuffle requirements.
    /// </summary>
    public interface IKeyDropShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}