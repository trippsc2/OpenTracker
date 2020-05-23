using OpenTracker.Models.Enums;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models
{
    public class SaveData
    {
        public Mode Mode { get; set; }
        public Dictionary<ItemType, int> ItemCounts { get; set; }
        public Dictionary<LocationID, Dictionary<int, int>> LocationSectionCounts { get; set; }
        public Dictionary<LocationID, Dictionary<int, MarkingType?>> LocationSectionMarkings { get; set; }
        public Dictionary<(LocationID, int), ItemType?> PrizePlacements { get; set; }
        public Dictionary<(LocationID, int), BossType?> BossPlacements { get; set; }
        public List<(LocationID, int, LocationID, int)> Connections { get; set; }

        public SaveData()
        {
        }

        public SaveData(Game game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            Mode = new Mode(game.Mode);

            ItemCounts = new Dictionary<ItemType, int>();
            LocationSectionCounts = new Dictionary<LocationID, Dictionary<int, int>>();
            LocationSectionMarkings = new Dictionary<LocationID, Dictionary<int, MarkingType?>>();
            PrizePlacements = new Dictionary<(LocationID, int), ItemType?>();
            BossPlacements = new Dictionary<(LocationID, int), BossType?>();
            Connections = new List<(LocationID, int, LocationID, int)>();

            foreach (Item item in game.Items.Values)
                ItemCounts.Add(item.Type, item.Current);

            foreach (Location location in game.Locations.Values)
            {
                for (int i = 0; i > location.BossSections.Count; i++)
                {
                    if (location.BossSections[i].Prize == null)
                        PrizePlacements.Add((location.ID, i), null);
                    else
                        PrizePlacements.Add((location.ID, i), location.BossSections[i].Prize.Type);
                    
                    if (location.BossSections[i].Boss == null)
                        BossPlacements.Add((location.ID, i), null);
                    else
                        BossPlacements.Add((location.ID, i), location.BossSections[i].Boss.Type);
                }

                LocationSectionCounts.Add(location.ID, new Dictionary<int, int>());
                LocationSectionMarkings.Add(location.ID, new Dictionary<int, MarkingType?>());

                for (int i = 0; i < location.Sections.Count; i++)
                {
                    Dictionary<int, int> countDictionary = LocationSectionCounts[location.ID];
                    Dictionary<int, MarkingType?> markingDictionary = LocationSectionMarkings[location.ID];

                    if (location.Sections[i].HasMarking)
                        markingDictionary.Add(i, location.Sections[i].Marking);

                    countDictionary.Add(i, location.Sections[i].Available);
                }
            }

            foreach ((MapLocation, MapLocation) connection in game.Connections)
            {
                int index1 = connection.Item1.Location.MapLocations.IndexOf(connection.Item1);
                int index2 = connection.Item2.Location.MapLocations.IndexOf(connection.Item2);

                Connections.Add((connection.Item1.Location.ID, index1, connection.Item2.Location.ID, index2));
            }
        }
    }
}
