using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Alternative
{
    /// <summary>
    /// This interface contains logic for a set of <see cref="IRequirement"/> alternatives.
    /// </summary>
    public interface IAlternativeRequirement : IRequirement
    {
        /// <summary>
        /// A factory for creating new <see cref="IAlternativeRequirement"/> objects.
        /// </summary>
        /// <param name="requirements">
        ///     A <see cref="IList{T}"/> of <see cref="IRequirement"/> alternatives.
        /// </param>
        /// <returns>
        ///     A new <see cref="IAlternativeRequirement"/> object.
        /// </returns>
        delegate IAlternativeRequirement Factory(IList<IRequirement> requirements);
    }
}