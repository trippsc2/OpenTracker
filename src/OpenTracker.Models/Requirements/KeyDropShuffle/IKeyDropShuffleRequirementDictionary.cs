using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.KeyDropShuffle;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IKeyDropShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public interface IKeyDropShuffleRequirementDictionary : IDictionary<bool, IRequirement>
{
}