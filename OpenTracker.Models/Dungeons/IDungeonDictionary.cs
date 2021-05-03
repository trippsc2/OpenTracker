using System;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    ///     This interface contains the dictionary container for dungeon data.
    /// </summary>
    public interface IDungeonDictionary : IDictionary<DungeonID, IDungeon>
    {
        /// <summary>
        ///     An event indicating that a new dungeon was created.
        /// </summary>
        event EventHandler<KeyValuePair<DungeonID, IDungeon>>? ItemCreated;
    }
}