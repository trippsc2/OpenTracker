using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for collecting data for save/load.
    /// </summary>
    public class SaveData
    {
        public Version? Version { get; set; }
        public ModeSaveData? Mode { get; set; }
        public Dictionary<ItemType, ItemSaveData>? Items { get; set; }
        public Dictionary<LocationID, LocationSaveData>? Locations { get; set; }
        public Dictionary<BossPlacementID, BossPlacementSaveData>? BossPlacements { get; set; }
        public Dictionary<PrizePlacementID, PrizePlacementSaveData>? PrizePlacements { get; set; }
        public List<ConnectionSaveData>? Connections { get; set; }
        public Dictionary<DropdownID, DropdownSaveData>? Dropdowns { get; set; }
        public List<LocationID>? PinnedLocations { get; set; }
    }
}
