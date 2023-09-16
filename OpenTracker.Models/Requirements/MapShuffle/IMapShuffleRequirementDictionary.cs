using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.MapShuffle;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IMapShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public interface IMapShuffleRequirementDictionary : IDictionary<bool, IRequirement>
{
}