using System.Collections.Generic;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    ///     This interface contains the dictionary container for world state requirements.
    /// </summary>
    public interface IWorldStateRequirementDictionary : IDictionary<WorldState, IRequirement>
    {
    }
}