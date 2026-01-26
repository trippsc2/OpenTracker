using System.Collections.Generic;

namespace OpenTracker.Models.Requirements.ShopShuffle;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IShopShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public interface IShopShuffleRequirementDictionary : IDictionary<bool, IRequirement>
{
}