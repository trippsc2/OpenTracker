using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Aggregate;

/// <summary>
/// This interface contains logic aggregating a set of <see cref="IRequirement"/>.
/// </summary>
public interface IAggregateRequirement : IRequirement
{
    /// <summary>
    /// A factory for creating new <see cref="IAggregateRequirement"/> objects.
    /// </summary>
    /// <param name="requirements">
    ///     A <see cref="IList{T}"/> of <see cref="IRequirement"/> to be aggregated.
    /// </param>
    /// <returns>
    ///     A new <see cref="IAggregateRequirement"/> object.
    /// </returns>
    delegate IAggregateRequirement Factory(IList<IRequirement> requirements);
}