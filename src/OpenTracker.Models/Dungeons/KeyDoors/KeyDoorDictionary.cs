using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Utils;

namespace OpenTracker.Models.Dungeons.KeyDoors;

/// <summary>
/// This class contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IKeyDoor"/> objects
/// indexed by <see cref="KeyDoorID"/>.
/// </summary>
public class KeyDoorDictionary : LazyDictionary<KeyDoorID, IKeyDoor>,
    IKeyDoorDictionary
{
    private readonly IMutableDungeon _dungeonData;
    private readonly Lazy<IKeyDoorFactory> _factory;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">
    ///     An Autofac factory for creating the <see cref="IKeyDoorFactory"/> object.
    /// </param>
    /// <param name="dungeonData">
    ///     The <see cref="IMutableDungeon"/> parent class.
    /// </param>
    public KeyDoorDictionary(IKeyDoorFactory.Factory factory, IMutableDungeon dungeonData)
        : base(new Dictionary<KeyDoorID, IKeyDoor>())
    {
        _dungeonData = dungeonData;
        _factory = new Lazy<IKeyDoorFactory>(() => factory());
    }

    public void PopulateDoors(IList<KeyDoorID> keyDoors)
    {
        foreach (var keyDoor in keyDoors)
        {
            _ = this[keyDoor];
        }
    }

    protected override IKeyDoor Create(KeyDoorID key)
    {
        return _factory.Value.GetKeyDoor(key, _dungeonData);
    }
}