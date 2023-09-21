using System.Collections.Generic;
using OpenTracker.Models.Items;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Requirements.Item.Exact;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="ItemExactRequirement"/>
/// objects indexed by <see cref="ItemType"/> and count.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ItemExactRequirementDictionary : LazyDictionary<(ItemType type, int count), IRequirement>, IItemExactRequirementDictionary
{
    private readonly IItemDictionary _items;
        
    private readonly ItemExactRequirement.Factory _factory;
        
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="items">
    ///     The <see cref="IItemDictionary"/>.
    /// </param>
    /// <param name="factory">
    ///     An Autofac factory for creating new <see cref="ItemExactRequirement"/> objects.
    /// </param>
    public ItemExactRequirementDictionary(IItemDictionary items, ItemExactRequirement.Factory factory)
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