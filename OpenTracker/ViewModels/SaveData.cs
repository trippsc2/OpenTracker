using OpenTracker.Models;
using OpenTracker.Models.Enums;
using System.Collections.Generic;

namespace OpenTracker.ViewModels
{
    public class SaveData
    {
        public Mode Mode { get; set; }
        public Dictionary<ItemType, int> ItemCounts { get; set; }
        public Dictionary<LocationID, Dictionary<int, int>> LocationSectionCounts { get; set; }
        public Dictionary<LocationID, Dictionary<int, MarkingType?>> LocationSectionMarkings { get; set; }
        public Dictionary<LocationID, ItemType?> PrizePlacements { get; set; }
        public Dictionary<LocationID, BossType?> BossPlacements { get; set; }

        public SaveData()
        {
        }

        public SaveData(Game game)
        {
            Mode = new Mode(game.Mode);

            ItemCounts = new Dictionary<ItemType, int>();
            LocationSectionCounts = new Dictionary<LocationID, Dictionary<int, int>>();
            LocationSectionMarkings = new Dictionary<LocationID, Dictionary<int, MarkingType?>>();
            PrizePlacements = new Dictionary<LocationID, ItemType?>();
            BossPlacements = new Dictionary<LocationID, BossType?>();

            foreach (Item item in game.Items.Values)
                ItemCounts.Add(item.Type, item.Current);

            foreach (Location location in game.Locations.Values)
            {
                if (location.BossSection != null)
                {
                    if (location.BossSection.Prize == null)
                        PrizePlacements.Add(location.ID, null);
                    else
                        PrizePlacements.Add(location.ID, location.BossSection.Prize.Type);

                    if (location.BossSection.Boss == null)
                        BossPlacements.Add(location.ID, null);
                    else
                        BossPlacements.Add(location.ID, location.BossSection.Boss.Type);
                }

                LocationSectionCounts.Add(location.ID, new Dictionary<int, int>());
                LocationSectionMarkings.Add(location.ID, new Dictionary<int, MarkingType?>());

                for (int i = 0; i < location.Sections.Count; i++)
                {
                    Dictionary<int, int> countDictionary = LocationSectionCounts[location.ID];
                    Dictionary<int, MarkingType?> markingDictionary = LocationSectionMarkings[location.ID];

                    if (location.Sections[i].HasMarking)
                        markingDictionary.Add(i, location.Sections[i].Marking);

                    switch (location.Sections[i])
                    {
                        case BossSection bossSection:

                            int bossAvailable = bossSection.Available ? 1 : 0;

                            countDictionary.Add(i, bossAvailable);

                            break;
                        case EntranceSection entranceSection:

                            int entranceAvailable = entranceSection.Available ? 1 : 0;

                            countDictionary.Add(i, entranceAvailable);

                            break;
                        case ItemSection itemSection:

                            countDictionary.Add(i, itemSection.Available);

                            break;
                    }
                }
            }
        }
    }
}
