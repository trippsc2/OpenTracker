using System.Collections.Generic;
using OpenTracker.Models.Items;

namespace OpenTracker.Models.Requirements.Item;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="ItemRequirement"/>
/// objects indexed by <see cref="ItemType"/> and count.
/// </summary>
public interface IItemRequirementDictionary : IDictionary<(ItemType type, int count), IRequirement>
{
}