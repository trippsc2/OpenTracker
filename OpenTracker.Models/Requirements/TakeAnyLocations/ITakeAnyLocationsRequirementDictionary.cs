using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.TakeAnyLocations
{
    /// <summary>
    /// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
    /// <see cref="ITakeAnyLocationsRequirement"/> objects indexed by <see cref="bool"/>.
    /// </summary>
    public interface ITakeAnyLocationsRequirementDictionary : IDictionary<bool, IRequirement>
    {
    }
}