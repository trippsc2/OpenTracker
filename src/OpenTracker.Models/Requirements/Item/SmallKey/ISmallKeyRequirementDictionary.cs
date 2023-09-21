using System.Collections.Generic;
using OpenTracker.Models.Dungeons;

namespace OpenTracker.Models.Requirements.Item.SmallKey;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="SmallKeyRequirement"/> objects indexed by <see cref="DungeonID"/> and count.
/// </summary>
public interface ISmallKeyRequirementDictionary : IDictionary<(DungeonID id, int count), IRequirement>
{
}