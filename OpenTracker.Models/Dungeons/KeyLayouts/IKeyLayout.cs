using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.State;

namespace OpenTracker.Models.Dungeons.KeyLayouts
{
    /// <summary>
    ///     This interface contains the key layout data.
    /// </summary>
    public interface IKeyLayout
    {
        /// <summary>
        ///     Returns whether the key layout is possible in the current game state.
        /// </summary>
        /// <param name="inaccessible">
        ///     A list of inaccessible locations for the dungeon.
        /// </param>
        /// <param name="accessible">
        ///     A list of accessible locations for the dungeon.
        /// </param>
        /// <param name="state">
        ///     The dungeon state data.
        /// </param>
        /// <returns>
        ///     A boolean representing whether the key layout is possible.
        /// </returns>
        bool CanBeTrue(IList<DungeonItemID> inaccessible, IList<DungeonItemID> accessible, IDungeonState state);
    }
}
