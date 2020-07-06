using OpenTracker.Models.Enums;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    /// <summary>
    /// This is the class for collecting data for save/load.
    /// </summary>
    public class SaveData
    {
        public Mode Mode { get; set; }
        public Dictionary<ItemType, int> ItemCounts { get; set; }
        public Dictionary<LocationID, Dictionary<int, int>> LocationSectionCounts { get; set; }
        public Dictionary<LocationID, Dictionary<int, MarkingType?>> LocationSectionMarkings { get; set; }
        public Dictionary<(LocationID, int), ItemType?> PrizePlacements { get; set; }
        public Dictionary<(LocationID, int), BossType?> BossPlacements { get; set; }
        public List<(LocationID, int, LocationID, int)> Connections { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="save">
        /// A boolean representing whether to save existing data.
        /// </param>
        public SaveData(bool save = false)
        {
            if (save)
            {
                Mode = Mode.Instance.Copy();
                ItemCounts = new Dictionary<ItemType, int>();
                LocationSectionCounts = new Dictionary<LocationID, Dictionary<int, int>>();
                LocationSectionMarkings = new Dictionary<LocationID, Dictionary<int, MarkingType?>>();
                PrizePlacements = new Dictionary<(LocationID, int), ItemType?>();
                BossPlacements = new Dictionary<(LocationID, int), BossType?>();
                Connections = new List<(LocationID, int, LocationID, int)>();

                foreach (var item in ItemDictionary.Instance.Values)
                {
                    ItemCounts.Add(item.Type, item.Current);
                }

                foreach (var location in LocationDictionary.Instance.Values)
                {
                    for (int i = 0; i > location.BossSections.Count; i++)
                    {
                        if (location.BossSections[i].Prize == null)
                        {
                            PrizePlacements.Add((location.ID, i), null);
                        }
                        else
                        {
                            PrizePlacements.Add((location.ID, i), location.BossSections[i].Prize.Type);
                        }

                        if (location.BossSections[i].BossPlacement.Boss == null)
                        {
                            BossPlacements.Add((location.ID, i), null);
                        }
                        else
                        {
                            BossPlacements.Add((location.ID, i), location.BossSections[i].BossPlacement.Boss);
                        }
                    }

                    LocationSectionCounts.Add(location.ID, new Dictionary<int, int>());
                    LocationSectionMarkings.Add(location.ID, new Dictionary<int, MarkingType?>());

                    for (int i = 0; i < location.Sections.Count; i++)
                    {
                        Dictionary<int, int> countDictionary = LocationSectionCounts[location.ID];
                        Dictionary<int, MarkingType?> markingDictionary = LocationSectionMarkings[location.ID];

                        if (location.Sections[i].HasMarking)
                        {
                            markingDictionary.Add(i, location.Sections[i].Marking);
                        }

                        countDictionary.Add(i, location.Sections[i].Available);
                    }
                }

                foreach ((MapLocation, MapLocation) connection in ConnectionCollection.Instance)
                {
                    int index1 = connection.Item1.Location.MapLocations.IndexOf(connection.Item1);
                    int index2 = connection.Item2.Location.MapLocations.IndexOf(connection.Item2);

                    Connections.Add((connection.Item1.Location.ID, index1, connection.Item2.Location.ID, index2));
                }
            }
        }
    }
}
