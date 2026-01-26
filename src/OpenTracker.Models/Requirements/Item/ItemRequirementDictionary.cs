using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Utils;

namespace OpenTracker.Models.Requirements.Item;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IItemRequirement"/>
/// objects indexed by <see cref="ItemType"/> and count.
/// </summary>
public class ItemRequirementDictionary : LazyDictionary<(ItemType type, int count), IRequirement>,
    IItemRequirementDictionary
{
    private readonly IItemDictionary _items;

    private readonly IItemRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="items">
    ///     The <see cref="IItemDictionary"/>.
    /// </param>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="IItemRequirement"/> objects.
    /// </param>
    public ItemRequirementDictionary(IItemDictionary items, IItemRequirement.Factory factory)
        : base(new Dictionary<(ItemType type, int count), IRequirement>())
    {
        _items = items;
            
        _factory = factory;
    }

    protected override IRequirement Create((ItemType type, int count) key)
    {
        return _factory(_items[key.type], key.count);
    }
}