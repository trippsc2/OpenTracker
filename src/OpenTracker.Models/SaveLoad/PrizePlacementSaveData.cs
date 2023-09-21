using OpenTracker.Models.Prizes;

namespace OpenTracker.Models.SaveLoad;

/// <summary>
/// This class contains prize placement save data.
/// </summary>
public sealed class PrizePlacementSaveData
{
    public PrizeType? Prize { get; init; }
}