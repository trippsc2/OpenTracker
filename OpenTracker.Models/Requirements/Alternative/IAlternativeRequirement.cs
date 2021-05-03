using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Alternative
{
    /// <summary>
    ///     This interface contains logic for a set of requirement alternatives.
    /// </summary>
    public interface IAlternativeRequirement : IRequirement
    {
        /// <summary>
        ///     A factory for creating a set of requirement alternatives.
        /// </summary>
        /// <param name="requirements">
        ///     A list of requirement alternatives.
        /// </param>
        /// <returns>
        ///     A new set of requirement alternatives.
        /// </returns>
        delegate IAlternativeRequirement Factory(IList<IRequirement> requirements);
    }
}