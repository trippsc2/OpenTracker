using System.Collections.Concurrent;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    /// <summary>
    /// This interface contains the logic for aggregating dungeon results into final values.
    /// </summary>
    public interface IResultAggregator
    {
        /// <summary>
        /// A factory for creating new <see cref="IResultAggregator"/> objects.
        /// </summary>
        /// <param name="dungeon">
        ///     The <see cref="IDungeon"/>.
        /// </param>
        /// <param name="mutableDungeonQueue">
        ///     The <see cref="IMutableDungeonQueue"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IResultAggregator"/> object.
        /// </returns>
        delegate IResultAggregator Factory(IDungeon dungeon, IMutableDungeonQueue mutableDungeonQueue);
        
        /// <summary>
        /// Returns the final <see cref="IDungeonResult"/>.
        /// </summary>
        /// <param name="finalQueue">
        ///     The <see cref="BlockingCollection{T}"/> queue for final <see cref="IDungeonState"/> permutations.
        /// </param>
        /// <returns>
        ///     The final <see cref="IDungeonState"/>.
        /// </returns>
        IDungeonResult AggregateResults(BlockingCollection<IDungeonState> finalQueue);
    }
}