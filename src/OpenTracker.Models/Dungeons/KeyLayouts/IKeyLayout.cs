using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.State;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    /// This interface contains the key layout data.
    /// </summary>
    public interface IKeyLayout
    {
        /// <summary>
        /// Returns whether the key layout is possible in the current game state.
        /// </summary>
        /// <param name="inaccessible">
        ///     A <see cref="IList{T}"/> of inaccessible <see cref="DungeonItemID"/> for the dungeon.
        /// </param>
        /// <param name="accessible">
        ///     A <see cref="IList{T}"/> of accessible <see cref="DungeonItemID"/> for the dungeon.
        /// </param>
        /// <param name="state">
        ///     The <see cref="IDungeonState"/>.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> representing whether the key layout is possible.
        /// </returns>
        bool CanBeTrue(IList<DungeonItemID> inaccessible, IList<DungeonItemID> accessible, IDungeonState state);
    }
}
