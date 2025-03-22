using System.Collections.Generic;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="IWorldStateRequirement"/> objects indexed by <see cref="WorldState"/>.
    /// </summary>
    public interface IWorldStateRequirementDictionary : IDictionary<WorldState, IRequirement>
    {
    }
}