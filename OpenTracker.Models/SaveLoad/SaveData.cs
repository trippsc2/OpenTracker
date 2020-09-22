using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Connections;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.SaveLoad.Converters;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This is the class for collecting data for save/load.
    /// </summary>
    public class SaveData
    {
        public Version Version { get; set; }
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
            Version = Assembly.GetExecutingAssembly().GetName().Version;
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
            if (Version.Major < 1 || (Version.Major == 1 &&
                (Version.Minor < 4 ||
                (Version.Minor == 4 && Version.Build <= 1))))
            {
                foreach (var location in Locations.Values)
                {
                    MarkingConverter.ConvertFrom141(location);
                }
            }

            Modes.Mode.Instance.Load(Mode);
            ItemDictionary.Instance.Load(Items);
            LocationDictionary.Instance.Load(Locations);
            BossPlacementDictionary.Instance.Load(BossPlacements);
            PrizePlacementDictionary.Instance.Load(PrizePlacements);
            ConnectionCollection.Instance.Load(Connections);
        }
    }
}
