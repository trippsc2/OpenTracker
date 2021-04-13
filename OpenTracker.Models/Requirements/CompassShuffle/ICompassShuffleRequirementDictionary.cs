using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.CompassShuffle
{
    /// <summary>
    ///     This interface contains the dictionary container for compass shuffle requirements.
    /// </summary>
    public interface ICompassShuffleRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}