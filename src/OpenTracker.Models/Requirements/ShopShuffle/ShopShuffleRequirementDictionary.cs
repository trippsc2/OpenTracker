using System.Collections.Generic;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.ShopShuffle;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for
/// <see cref="ShopShuffleRequirement"/> objects indexed by <see cref="bool"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ShopShuffleRequirementDictionary : LazyDictionary<bool, IRequirement>,
    IShopShuffleRequirementDictionary
{
    private readonly ShopShuffleRequirement.Factory _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="ShopShuffleRequirement"/> objects.
    /// </param>
    public ShopShuffleRequirementDictionary(ShopShuffleRequirement.Factory factory)
        : base(new Dictionary<bool, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(bool key)
    {
        return _factory(key);
    }
}