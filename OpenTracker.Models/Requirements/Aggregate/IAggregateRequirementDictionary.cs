using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Aggregate
{
    /// <summary>
    ///     This interface contains the dictionary container for requirements aggregating a set of requirements.
    /// </summary>
    public interface IAggregateRequirementDictionary : IDictionary<HashSet<IRequirement>, IRequirement>
    {
    }
}