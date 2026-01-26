using System;
using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Mutable;

namespace OpenTracker.Models.Dungeons.KeyDoors;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IKeyDoor"/> objects
/// indexed by <see cref="KeyDoorID"/>.
/// </summary>
public interface IKeyDoorDictionary : IDictionary<KeyDoorID, IKeyDoor>
{
    /// <summary>
    /// An event indicating that a new <see cref="IKeyDoor"/> was created.
    /// </summary>
    event EventHandler<KeyValuePair<KeyDoorID, IKeyDoor>> ItemCreated;

    /// <summary>
    /// A factory for creating new <see cref="IKeyDoorDictionary"/> objects.
    /// </summary>
    /// <param name="dungeonData">
    ///     The <see cref="IMutableDungeon"/> parent class.
    /// </param>
    /// <returns>
    ///     A new <see cref="IKeyDoorDictionary"/> object.
    /// </returns>
    delegate IKeyDoorDictionary Factory(IMutableDungeon dungeonData);

    /// <summary>
    /// Calls the indexer for each <see cref="IKeyDoor"/> in the specified <see cref="IList{T}"/>, so that it is
    /// initialized.
    /// </summary>
    /// <param name="keyDoors">
    ///     A <see cref="IList{T}"/> of <see cref="KeyDoorID"/> for which to call.
    /// </param>
    void PopulateDoors(IList<KeyDoorID> keyDoors);
}