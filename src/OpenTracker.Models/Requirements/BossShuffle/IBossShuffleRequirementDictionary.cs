using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.BossShuffle;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IBossShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public interface IBossShuffleRequirementDictionary : IDictionary<bool, IRequirement>
{
}