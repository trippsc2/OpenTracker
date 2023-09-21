using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.Items.Factories;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.Models.Items;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IItem"/> objects
/// indexed by <see cref="ItemType"/>.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class ItemDictionary : LazyDictionary<ItemType, IItem>, IItemDictionary
{
    private readonly Lazy<IItemFactory> _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating the <see cref="IItemFactory"/> object.
    /// </param>
    public ItemDictionary(IItemFactory.Factory factory) : base(new Dictionary<ItemType, IItem>())
    {
        _factory = new Lazy<IItemFactory>(() => factory());
    }

    public void Reset()
    {
        foreach (var item in Values)
        {
            item.Reset();
        }
    }

    protected override IItem Create(ItemType key)
    {
        return _factory.Value.GetItem(key);
    }

    public IDictionary<ItemType, ItemSaveData> Save()
    {
        return Keys.ToDictionary(type => type, type => this[type].Save());
    }

    public void Load(IDictionary<ItemType, ItemSaveData>? saveData)
    {
        if (saveData == null)
        {
            return;
        }

        foreach (var item in saveData.Keys)
        {
            this[item].Load(saveData[item]);
        }
    }
}