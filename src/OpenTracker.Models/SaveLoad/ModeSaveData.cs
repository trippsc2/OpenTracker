using OpenTracker.Models.Modes;

namespace OpenTracker.Models.SaveLoad;

/// <summary>
/// This class contains mode setting save data.
/// </summary>
public class ModeSaveData
{
    public ItemPlacement ItemPlacement { get; init; } = ItemPlacement.Advanced;
    public bool MapShuffle { get; init; }
    public bool CompassShuffle { get; init; }
    public bool SmallKeyShuffle { get; init; }
    public bool BigKeyShuffle { get; init; }
    public WorldState WorldState { get; init; }
    public EntranceShuffle EntranceShuffle { get; init; }
    public bool BossShuffle { get; init; }
    public bool EnemyShuffle { get; init; }
    public bool GuaranteedBossItems { get; init; }
    public bool GenericKeys { get; init; }
    public bool TakeAnyLocations { get; init; }
    public bool KeyDropShuffle { get; init; }
    public bool ShopShuffle { get; init; }
}