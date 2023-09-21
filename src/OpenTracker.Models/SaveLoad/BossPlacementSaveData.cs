using OpenTracker.Models.BossPlacements;

namespace OpenTracker.Models.SaveLoad;

/// <summary>
/// This class contains boss placement save data.
/// </summary>
public sealed class BossPlacementSaveData
{
    public BossType? Boss { get; init; }
}