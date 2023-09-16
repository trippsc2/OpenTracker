using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons;

/// <summary>
/// This interface contains the <see cref="IDictionary{TKey,TValue}"/> container for <see cref="IDungeon"/> objects
/// indexed by <see cref="DungeonID"/>.
/// </summary>
public interface IDungeonDictionary : IDictionary<DungeonID, IDungeon>
{
    /// <summary>
    /// An event indicating that a new <see cref="IDungeon"/> object was created.
    /// </summary>
    event EventHandler<KeyValuePair<DungeonID, IDungeon>>? ItemCreated;
}