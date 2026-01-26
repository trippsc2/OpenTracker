using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.CompassShuffle;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="ICompassShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public interface ICompassShuffleRequirementDictionary : IDictionary<bool, IRequirement>
{
}