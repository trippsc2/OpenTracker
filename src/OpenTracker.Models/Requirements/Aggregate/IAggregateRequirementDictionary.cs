using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.Aggregate;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IRequirement"/>
/// indexed by <see cref="HashSet{T}"/> of <see cref="IRequirement"/>.
/// </summary>
public interface IAggregateRequirementDictionary : IDictionary<HashSet<IRequirement>, IRequirement>
{
}