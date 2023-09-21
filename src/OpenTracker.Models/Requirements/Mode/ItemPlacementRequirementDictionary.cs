using System.Collections.Generic;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Mode;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for the
/// <see cref="ItemPlacementRequirement"/> objects indexed by <see cref="ItemPlacement"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ItemPlacementRequirementDictionary : LazyDictionary<ItemPlacement, IRequirement>, IItemPlacementRequirementDictionary
{
    private readonly ItemPlacementRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="ItemPlacementRequirement"/> objects.
    /// </param>
    public ItemPlacementRequirementDictionary(ItemPlacementRequirement.Factory factory)
        : base(new Dictionary<ItemPlacement, IRequirement>())
    {
        _factory = factory;
    }

    protected override IRequirement Create(ItemPlacement key)
    {
        return _factory(key);
    }
}