using System.Collections.Generic;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This interface contains the dictionary container for entrance shuffle requirements.
    /// </summary>
    public interface IEntranceShuffleRequirementDictionary : IDictionary<EntranceShuffle, IRequirement>
    {
    }
}