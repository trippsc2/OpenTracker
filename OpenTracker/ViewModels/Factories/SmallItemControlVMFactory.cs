using Avalonia.Layout;
using OpenTracker.Models;
using OpenTracker.Models.Enums;
using OpenTracker.Models.Requirements;
using OpenTracker.ViewModels.Bases;
using OpenTracker.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Factories
{
    public static class SmallItemControlVMFactory
    {
        internal static void GetSmallItemControlVMs(
            UndoRedoManager undoRedoManager, AppSettings appSettings, ItemsPanelControlVM itemsPanel,
            LocationID location, ObservableCollection<SmallItemControlVMBase> smallItems)
        {
            if (smallItems == null)
            {
                throw new ArgumentNullException(nameof(smallItems));
            }

            switch (location)
            {
                case LocationID.HyruleCastle:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.HCSmallKey]));
                        smallItems.Add(new SmallItemSpacerControlVM(
                            new AggregateRequirement(new List<IRequirement>
                            {
                                new ItemsPanelOrientationRequirement(itemsPanel, Orientation.Vertical),
                                RequirementDictionary.Instance[RequirementType.DungeonItemShuffleKeysanity]
                            })));
                        smallItems.Add(new DungeonChestControlVM(
                            undoRedoManager, appSettings, LocationDictionary.Instance[location].Sections[0]));
                    }
                    break;
                case LocationID.AgahnimTower:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.ATSmallKey]));
                        smallItems.Add(new SmallItemSpacerControlVM(
                            new AggregateRequirement(new List<IRequirement>
                            {
                                new ItemsPanelOrientationRequirement(itemsPanel, Orientation.Vertical),
                                RequirementDictionary.Instance[RequirementType.DungeonItemShuffleKeysanity]
                            })));
                        smallItems.Add(new DungeonChestControlVM(
                            undoRedoManager, appSettings, LocationDictionary.Instance[location].Sections[0]));
                    }
                    break;
                case LocationID.EasternPalace:
                    {
                        smallItems.Add(new SmallItemSpacerControlVM(
                            RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.EPBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.DesertPalace:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.DPSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.DPBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.TowerOfHera:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.DPSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.DPBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.PalaceOfDarkness:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.ToHSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.ToHBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.SwampPalace:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.SPSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.SPBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.SkullWoods:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.SWSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.SWBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.ThievesTown:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.TTSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.TTBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.IcePalace:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.IPSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.IPBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.MiseryMire:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.MMSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.MMBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.TurtleRock:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.TRSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.TRBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new PrizeControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                    }
                    break;
                case LocationID.GanonsTower:
                    {
                        smallItems.Add(new SmallKeyControlVM(
                            undoRedoManager, appSettings, ItemDictionary.Instance[ItemType.GTSmallKey]));
                        smallItems.Add(new BigKeyControlVM(
                            undoRedoManager, ItemDictionary.Instance[ItemType.GTBigKey]));
                        smallItems.Add(new DungeonChestControlVM(undoRedoManager, appSettings,
                            LocationDictionary.Instance[location].Sections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[0]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[1]));
                        smallItems.Add(new BossControlVM(
                            undoRedoManager, LocationDictionary.Instance[location].BossSections[2]));
                    }
                    break;
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(location));
                    }
            }
        }
    }
}
