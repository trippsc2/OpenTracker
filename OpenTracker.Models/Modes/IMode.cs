using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.UndoRedo.Mode;
using ReactiveUI;

namespace OpenTracker.Models.Modes;

/// <summary>
/// This interface contains game mode settings data.
/// </summary>
public interface IMode : IReactiveObject, ISaveable<ModeSaveData>
{
    /// <summary>
    /// The <see cref="ItemPlacement"/> mode setting.
    /// </summary>
    ItemPlacement ItemPlacement { get; set; }

    /// <summary>
    /// A <see cref="bool"/> representing whether map shuffle is enabled.
    /// </summary>
    bool MapShuffle { get; set; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether compass shuffle is enabled.
    /// </summary>
    bool CompassShuffle { get; set; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether small key shuffle is enabled.
    /// </summary>
    bool SmallKeyShuffle { get; set; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether big key shuffle is enabled.
    /// </summary>
    bool BigKeyShuffle { get; set; }
        
    /// <summary>
    /// The <see cref="WorldState"/> mode setting.
    /// </summary>
    WorldState WorldState { get; set; }
        
    /// <summary>
    /// The <see cref="EntranceShuffle"/> mode setting.
    /// </summary>
    EntranceShuffle EntranceShuffle { get; set; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether boss shuffle is enabled.
    /// </summary>
    bool BossShuffle { get; set; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether enemy shuffle is enabled.
    /// </summary>
    bool EnemyShuffle { get; set; }

    /// <summary>
    /// A <see cref="bool"/> representing whether bosses are guaranteed to not have a dungeon item.
    /// </summary>
    bool GuaranteedBossItems { get; set; }

    /// <summary>
    /// A <see cref="bool"/> representing whether generic keys are enabled.
    /// </summary>
    bool GenericKeys { get; set; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether take any locations are enabled.
    /// </summary>
    bool TakeAnyLocations { get; set; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether key drop shuffle is enabled.
    /// </summary>
    bool KeyDropShuffle { get; set; }
        
    /// <summary>
    /// A <see cref="bool"/> representing whether shop shuffle is enabled.
    /// </summary>
    bool ShopShuffle { get; set; }
        
    /// <summary>
    /// Returns a new <see cref="IChangeItemPlacement"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     The new <see cref="ItemPlacement"/> value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeItemPlacement"/> object.
    /// </returns>
    IUndoable CreateChangeItemPlacementAction(ItemPlacement newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeMapShuffle"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new map shuffle value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeMapShuffle"/> object.
    /// </returns>
    IUndoable CreateChangeMapShuffleAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeCompassShuffle"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new compass shuffle value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeCompassShuffle"/> object.
    /// </returns>
    IUndoable CreateChangeCompassShuffleAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeSmallKeyShuffle"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new small key shuffle value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeSmallKeyShuffle"/> object.
    /// </returns>
    IUndoable CreateChangeSmallKeyShuffleAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeBigKeyShuffle"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new big key shuffle value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeBigKeyShuffle"/> object.
    /// </returns>
    IUndoable CreateChangeBigKeyShuffleAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeWorldState"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     The new <see cref="WorldState"/> value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeWorldState"/> object.
    /// </returns>
    IUndoable CreateChangeWorldStateAction(WorldState newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeEntranceShuffle"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     The new <see cref="EntranceShuffle"/> value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeEntranceShuffle"/> object.
    /// </returns>
    IUndoable CreateChangeEntranceShuffleAction(EntranceShuffle newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeBossShuffle"/>.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing new boss shuffle value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeBossShuffle"/>.
    /// </returns>
    IUndoable CreateChangeBossShuffleAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeEnemyShuffle"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new enemy shuffle value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeEnemyShuffle"/> object.
    /// </returns>
    IUndoable CreateChangeEnemyShuffleAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeGuaranteedBossItems"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new guaranteed boss items value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeGuaranteedBossItems"/> object.
    /// </returns>
    IUndoable CreateChangeGuaranteedBossItemsAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeGenericKeys"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new generic keys value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeGenericKeys"/> object.
    /// </returns>
    IUndoable CreateChangeGenericKeysAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeTakeAnyLocations"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new take any locations value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeTakeAnyLocations"/> object.
    /// </returns>
    IUndoable CreateChangeTakeAnyLocationsAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeKeyDropShuffle"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new key drop shuffle value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeKeyDropShuffle"/> object.
    /// </returns>
    IUndoable CreateChangeKeyDropShuffleAction(bool newValue);

    /// <summary>
    /// Returns a new <see cref="IChangeShopShuffle"/> object.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new shop shuffle value.
    /// </param>
    /// <returns>
    ///     The new <see cref="IChangeShopShuffle"/> object.
    /// </returns>
    IUndoable CreateChangeShopShuffleAction(bool newValue);
}