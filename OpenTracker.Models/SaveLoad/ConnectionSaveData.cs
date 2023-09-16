using OpenTracker.Models.Locations;

namespace OpenTracker.Models.SaveLoad;

/// <summary>
/// This class contains connection save data.
/// </summary>
public class ConnectionSaveData
{
    public LocationID Location1 { get; init; }
    public LocationID Location2 { get; init; }
    public int Index1 { get; init; }
    public int Index2 { get; init; }
}