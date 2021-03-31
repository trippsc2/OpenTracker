using System.Collections.Concurrent;
using System.Collections.Generic;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    /// <summary>
    ///     This interface contains the logic for aggregating dungeon results into final values.
    /// </summary>
    public interface IResultAggregator
    {
        /// <summary>
        ///     A factory for creating result aggregators.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <param name="mutableDungeonQueue">
        ///     The mutable dungeon data instance queue.
        /// </param>
        /// <returns>
        ///     A new result aggregator.
        /// </returns>
        delegate IResultAggregator Factory(IDungeon dungeon, IMutableDungeonQueue mutableDungeonQueue);
        
        /// <summary>
        ///     Returns a tuple containing final boss and item accessibility values.
        /// </summary>
        /// <param name="finalQueue">
        ///     The blocking collection queue for final key door permutations.
        /// </param>
        /// <returns>
        ///     A tuple containing final boss and item accessibility values.
        /// </returns>
        (List<AccessibilityLevel> bossAccessibility, bool visible, bool sequenceBreak, int accessible) AggregateResults(
            BlockingCollection<IDungeonState> finalQueue);
    }
}