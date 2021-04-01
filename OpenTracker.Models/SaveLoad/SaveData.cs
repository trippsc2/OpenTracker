using System;
using System.Collections.Generic;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This class contains save data.
    /// </summary>
    public class SaveData
    {
        public Version? Version { get; set; }
        public ModeSaveData? Mode { get; set; }
        public IDictionary<ItemType, ItemSaveData>? Items { get; set; }
        public IDictionary<LocationID, LocationSaveData>? Locations { get; set; }
        public IDictionary<BossPlacementID, BossPlacementSaveData>? BossPlacements { get; set; }
        public IDictionary<PrizePlacementID, PrizePlacementSaveData>? PrizePlacements { get; set; }
        public IList<ConnectionSaveData>? Connections { get; set; }
        public IDictionary<DropdownID, DropdownSaveData>? Dropdowns { get; set; }
        public IList<LocationID>? PinnedLocations { get; set; }
    }
}
