using System.Collections.Generic;
using OpenTracker.Models.Modes;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="EntranceShuffleRequirement"/> objects indexed by <see cref="EntranceShuffle"/>.
/// </summary>
public interface IEntranceShuffleRequirementDictionary : IDictionary<EntranceShuffle, IRequirement>
{
}