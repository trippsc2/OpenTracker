using System.Collections.Concurrent;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Result;
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
        ///     Returns the final result.
        /// </summary>
        /// <param name="finalQueue">
        ///     The blocking collection queue for final key door permutations.
        /// </param>
        /// <returns>
        ///     The final result.
        /// </returns>
        IDungeonResult AggregateResults(BlockingCollection<IDungeonState> finalQueue);
    }
}