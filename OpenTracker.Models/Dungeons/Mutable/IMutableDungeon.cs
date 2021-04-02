using System.Collections.Generic;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.Nodes;
using OpenTracker.Models.Dungeons.Result;
using OpenTracker.Models.Dungeons.State;
using OpenTracker.Models.KeyDoors;

namespace OpenTracker.Models.Dungeons.Mutable
{
    /// <summary>
    ///     This interface contains the mutable dungeon data.
    /// </summary>
    public interface IMutableDungeon
    {
        /// <summary>
        ///     The dictionary of nodes by ID.
        /// </summary>
        IDungeonNodeDictionary Nodes { get; }
        
        /// <summary>
        ///     The dictionary of items by ID.
        /// </summary>
        IDungeonItemDictionary DungeonItems { get; }
        
        /// <summary>
        ///     The dictionary of key doors by ID.
        /// </summary>
        IKeyDoorDictionary KeyDoors { get; }

        /// <summary>
        ///     A factory for creating mutable dungeon data.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon immutable data.
        /// </param>
        /// <returns>
        ///     A new mutable dungeon data instance.
        /// </returns>
        delegate IMutableDungeon Factory(IDungeon dungeon);

        /// <summary>
        ///     Initializes the dictionary data classes for this dungeon.
        /// </summary>
        void InitializeData();

        /// <summary>
        ///     Applies the dungeon state conditions to this instance.
        /// </summary>
        /// <param name="state">
        ///     The dungeon state to be applied.
        /// </param>
        void ApplyState(IDungeonState state);

        /// <summary>
        ///     Returns the number of keys that are available to be collected in the dungeon.
        /// </summary>
        /// <param name="sequenceBreak">
        ///     A boolean representing whether sequence breaking is allowed for this count.
        /// </param>
        /// <returns>
        ///     A 32-bit integer representing the number of keys that are available to be collected in the
        ///         dungeon.
        /// </returns>
        int GetAvailableSmallKeys(bool sequenceBreak = false);
        
        /// <summary>
        ///     Returns a list of accessible key doors in the dungeon.
        /// </summary>
        /// <param name="sequenceBreak">
        ///     A boolean representing whether to return key doors only accessible by sequence break.
        /// </param>
        /// <returns>
        ///     A list of accessible key doors.
        /// </returns>
        IList<KeyDoorID> GetAccessibleKeyDoors(bool sequenceBreak = false);
        
        /// <summary>
        ///     Returns whether the specified number of collected keys and big key can occur, based on key logic.
        /// </summary>
        /// <param name="state">
        ///     The dungeon state data.
        /// </param>
        /// <returns>
        ///     A boolean representing whether the result can occur.
        /// </returns>
        bool ValidateKeyLayout(IDungeonState state);

        /// <summary>
        ///     Returns the current accessibility and accessible item count based on the specified dungeon state.
        /// </summary>
        /// <param name="state">
        ///     The dungeon state data.
        /// </param>
        /// <returns>
        ///     The dungeon result data.
        /// </returns>
        IDungeonResult GetDungeonResult(IDungeonState state);
    }
}