using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Aggregate
{
    /// <summary>
    ///     This interface contains logic aggregating a set of requirements.
    /// </summary>
    public interface IAggregateRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating new requirements aggregating a set of requirements.
        /// </summary>
        /// <param name="requirements">
        ///     A list of requirements to be aggregated.
        /// </param>
        /// <returns>
        ///     A new requirement aggregating a set of requirements.
        /// </returns>
        delegate IAggregateRequirement Factory(IList<IRequirement> requirements);
    }
}