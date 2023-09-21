using System.Collections.Concurrent;
using OpenTracker.Models.Dungeons.Mutable;
using OpenTracker.Models.Dungeons.State;

namespace OpenTracker.Models.Dungeons.AccessibilityProvider;

/// <summary>
/// This interface contains the logic for iterating through key doors in a dungeon.
/// </summary>
public interface IKeyDoorIterator
{
    /// <summary>
    /// A factory for creating new <see cref="IKeyDoorIterator"/> objects.
    /// </summary>
    /// <param name="dungeon">
    ///     The <see cref="IDungeon"/>.
    /// </param>
    /// <param name="mutableDungeonQueue">
    ///     The <see cref="IMutableDungeonQueue"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IKeyDoorIterator"/> object.
    /// </returns>
    delegate IKeyDoorIterator Factory(IDungeon dungeon, IMutableDungeonQueue mutableDungeonQueue);
        
    /// <summary>
    /// Processes all key door permutations and places final permutations into a queue.
    /// </summary>
    /// <param name="finalQueue">
    ///     The <see cref="BlockingCollection{T}"/> of final <see cref="IDungeonState"/> permutations.
    /// </param>
    void ProcessKeyDoorPermutations(BlockingCollection<IDungeonState> finalQueue);
}