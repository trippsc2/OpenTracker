using System.Collections.Concurrent;
using System.Threading.Tasks;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider
{
    /// <summary>
    ///     This interface contains the logic for iterating through key doors in a dungeon.
    /// </summary>
    public interface IKeyDoorIterator
    {
        /// <summary>
        ///     A factory for creating key door iterators.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon data.
        /// </param>
        /// <param name="mutableDungeonQueue">
        ///     The queue of mutable dungeon data instances.
        /// </param>
        /// <returns>
        ///     A new key door iterator.
        /// </returns>
        delegate IKeyDoorIterator Factory(IDungeon dungeon, IMutableDungeonQueue mutableDungeonQueue);
        
        /// <summary>
        ///     Processes all key door permutations and places final permutations into a queue.
        /// </summary>
        /// <param name="finalQueue">
        ///     The blocking collection queue of final permutations.
        /// </param>
        void ProcessKeyDoorPermutations(BlockingCollection<IDungeonState> finalQueue);
    }
}