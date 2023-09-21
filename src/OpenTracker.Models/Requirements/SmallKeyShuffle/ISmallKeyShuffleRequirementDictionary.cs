using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.SmallKeyShuffle;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="SmallKeyShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public interface ISmallKeyShuffleRequirementDictionary : IDictionary<bool, IRequirement>
{
}