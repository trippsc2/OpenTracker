using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;

namespace OpenTracker.Models.Modes
{
    /// <summary>
    /// This interface contains game mode settings data.
    /// </summary>
    public interface IMode : IReactiveObject
    {
        bool BigKeyShuffle { get; set; }
        bool BossShuffle { get; set; }
        bool CompassShuffle { get; set; }
        bool EnemyShuffle { get; set; }
        EntranceShuffle EntranceShuffle { get; set; }
        bool GenericKeys { get; set; }
        bool GuaranteedBossItems { get; set; }
        ItemPlacement ItemPlacement { get; set; }
        bool KeyDropShuffle { get; set; }
        bool MapShuffle { get; set; }
        bool ShopShuffle { get; set; }
        bool SmallKeyShuffle { get; set; }
        bool TakeAnyLocations { get; set; }
        WorldState WorldState { get; set; }

        void Load(ModeSaveData? saveData);
        ModeSaveData Save();

        /// <summary>
        /// Returns a new undoable action changing the item placement setting.
        /// </summary>
        /// <param name="newValue">
        /// The new item placement value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeItemPlacementAction(ItemPlacement newValue);

        /// <summary>
        /// Returns a new undoable action changing the map shuffle setting.
        /// </summary>
        /// <param name="newValue">
        /// The new map shuffle value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeMapShuffleAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the compass shuffle setting.
        /// </summary>
        /// <param name="newValue">
        /// The new compass shuffle value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeCompassShuffleAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the small key shuffle setting.
        /// </summary>
        /// <param name="newValue">
        /// The new small key shuffle value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeSmallKeyShuffleAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the big key shuffle setting.
        /// </summary>
        /// <param name="newValue">
        /// The new big key shuffle value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeBigKeyShuffleAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the world state setting.
        /// </summary>
        /// <param name="newValue">
        /// The new world state value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeWorldStateAction(WorldState newValue);

        /// <summary>
        /// Returns a new undoable action changing the entrance shuffle setting.
        /// </summary>
        /// <param name="newValue">
        /// The new entrance shuffle value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeEntranceShuffleAction(EntranceShuffle newValue);

        /// <summary>
        /// Returns a new undoable action changing the boss shuffle setting.
        /// </summary>
        /// <param name="newValue">
        /// The new boss shuffle value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeBossShuffleAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the enemy shuffle setting.
        /// </summary>
        /// <param name="newValue">
        /// The new enemy shuffle value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeEnemyShuffleAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the guaranteed boss items setting.
        /// </summary>
        /// <param name="newValue">
        /// The new guaranteed boss items value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeGuaranteedBossItemsAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the generic keys setting.
        /// </summary>
        /// <param name="newValue">
        /// The new generic keys value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeGenericKeysAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the take any locations setting.
        /// </summary>
        /// <param name="newValue">
        /// The new take any locations value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeTakeAnyLocationsAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the key drop shuffle setting.
        /// </summary>
        /// <param name="newValue">
        /// The new key drop shuffle value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeKeyDropShuffleAction(bool newValue);

        /// <summary>
        /// Returns a new undoable action changing the shop shuffle setting.
        /// </summary>
        /// <param name="newValue">
        /// The new shop shuffle value.
        /// </param>
        /// <returns>
        /// The new undoable action.
        /// </returns>
        IUndoable CreateChangeShopShuffleAction(bool newValue);
    }
}