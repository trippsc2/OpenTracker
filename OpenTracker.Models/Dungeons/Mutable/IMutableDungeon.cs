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

        delegate IMutableDungeon Factory(IDungeon dungeon);

        /// <summary>
        ///     Applies the dungeon state conditions to this instance.
        /// </summary>
        /// <param name="state">
        ///     The dungeon state to be applied.
        /// </param>
        void ApplyState(IDungeonState state);
        
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
        int GetAvailableSmallKeys(bool sequenceBreak = false);
        IDungeonResult GetDungeonResult(IDungeonState state);
        bool ValidateKeyLayout(IDungeonState state);
        void Reset();

        /// <summary>
        /// Initialize all data in this dungeon data instance.
        /// </summary>
        void InitializeData();
    }
}