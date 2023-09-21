namespace OpenTracker.Models.SaveLoad;

/// <summary>
/// This class contains item save data.
/// </summary>
public sealed class ItemSaveData
{
    public int Current { get; init; }
    public bool Known { get; set; }
}