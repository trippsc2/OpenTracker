using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for collecting data for save/load.
    /// </summary>
    public class SaveData
    {
        public ModeSaveData Mode { get; set; }
        public Dictionary<ItemType, ItemSaveData> Items { get; set; }
        public Dictionary<LocationID, LocationSaveData> Locations { get; set; }
        public Dictionary<BossPlacementID, BossPlacementSaveData> BossPlacements { get; set; }
        public Dictionary<PrizePlacementID, PrizePlacementSaveData> PrizePlacements { get; set; }
        public List<ConnectionSaveData> Connections { get; set; }

        /// <summary>
        /// Saves data to this class.
        /// </summary>
        public void Save()
        {
            Mode = Modes.Mode.Instance.Save();
            Items = ItemDictionary.Instance.Save();
            Locations = LocationDictionary.Instance.Save();
            BossPlacements = BossPlacementDictionary.Instance.Save();
            PrizePlacements = PrizePlacementDictionary.Instance.Save();
            Connections = ConnectionCollection.Instance.Save();
        }

        /// <summary>
        /// Loads data from this class.
        /// </summary>
        public void Load()
        {
            Modes.Mode.Instance.Load(Mode);
            ItemDictionary.Instance.Load(Items);
            LocationDictionary.Instance.Load(Locations);
            BossPlacementDictionary.Instance.Load(BossPlacements);
            PrizePlacementDictionary.Instance.Load(PrizePlacements);
            ConnectionCollection.Instance.Load(Connections);
        }
    }
}
