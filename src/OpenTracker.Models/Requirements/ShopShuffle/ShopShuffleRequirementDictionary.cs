using System.Collections.Generic;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.ShopShuffle;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="IShopShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
public class ShopShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>,
    IShopShuffleRequirementDictionary
{
    private readonly IShopShuffleRequirement.Factory _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IShopShuffleRequirement"/> objects.
    /// </param>
    public ShopShuffleRequirementDictionary(IShopShuffleRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}