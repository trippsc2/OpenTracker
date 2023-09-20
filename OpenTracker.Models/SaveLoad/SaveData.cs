using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.SaveLoad;

/// <summary>
/// This class contains save data.
/// </summary>
public sealed class SaveData
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Version? Version { get; init; }
    public ModeSaveData? Mode { get; init; }
    public IDictionary<ItemType, ItemSaveData>? Items { get; init; }
    public IDictionary<LocationID, LocationSaveData>? Locations { get; init; }
    public IDictionary<BossPlacementID, BossPlacementSaveData>? BossPlacements { get; init; }
    public IDictionary<PrizePlacementID, PrizePlacementSaveData>? PrizePlacements { get; init; }
    public IList<ConnectionSaveData>? Connections { get; init; }
    public IDictionary<DropdownID, DropdownSaveData>? Dropdowns { get; init; }
    public IList<LocationID>? PinnedLocations { get; init; }
}