using Avalonia.Layout;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the class containing creation logic for small item control ViewModel classes.
    /// </summary>
    internal static class SmallItemVMFactory
    {
        /// <summary>
        /// Returns a new small item control ViewModel instance representing a small key.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static SmallKeySmallItemVM GetSmallKeySmallItemControlVM(
            ItemType type)
        {
            return new SmallKeySmallItemVM(ItemDictionary.Instance[type]);
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a spacer.
        /// </summary>
        /// <param name="itemsPanel">
        /// The items panel parent class.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static SpacerSmallItemVM GetSpacerSmallItemControlVM(
            IRequirement requirement)
        {
            return new SpacerSmallItemVM(requirement);
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing dungeon items.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static DungeonItemSmallItemVM GetDungeonItemSmallItemControlVM(
            LocationID id)
        {
            return new DungeonItemSmallItemVM(LocationDictionary.Instance[id].Sections[0]);
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a dungeon prize.
        /// </summary>
        /// <param name="id">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static PrizeSmallItemVM GetPrizeSmallItemControlVM(LocationID id)
        {
            return new PrizeSmallItemVM(
                (IPrizeSection)LocationDictionary.Instance[id].Sections[1]);
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a big key.
        /// </summary>
        /// <param name="type">
        /// The item type to be represented.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static BigKeySmallItemVM GetBigKeySmallItemControlVM(ItemType type)
        {
            return new BigKeySmallItemVM(ItemDictionary.Instance[type]);
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a boss.
        /// </summary>
        /// <param name="id">
        /// The boss placement ID to be represented.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static BossSmallItemVM GetBossSmallItemControlVM(BossPlacementID id)
        {
            return new BossSmallItemVM(BossPlacementDictionary.Instance[id]);
        }

        /// <summary>
        /// Populates an observable collection of small item control ViewModel instances for the
        /// specified location.
        /// </summary>
        /// <param name="location">
        /// The location ID.
        /// </param>
        /// <param name="itemsPanel">
        /// The Items panel control ViewModel parent class.
        /// </param>
        /// <param name="smallItems">
        /// The observable collection of small item control ViewModel instances to be populated.
        /// </param>
        internal static ObservableCollection<SmallItemVMBase> GetSmallItemControlVMs(
            LocationID location)
        {
            var smallItems = new ObservableCollection<SmallItemVMBase>();

            switch (location)
            {
                case LocationID.HyruleCastle:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.HCSmallKey));
                        smallItems.Add(GetSpacerSmallItemControlVM(new AggregateRequirement(
                            new List<IRequirement>
                            {
                                new ItemsPanelOrientationRequirement(Orientation.Vertical),
                                RequirementDictionary.Instance[RequirementType.DungeonItemShuffleKeysanity]
                            })));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                    }
                    break;
                case LocationID.AgahnimTower:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.ATSmallKey));
                        smallItems.Add(GetSpacerSmallItemControlVM(new AggregateRequirement(
                            new List<IRequirement>
                            {
                                new ItemsPanelOrientationRequirement(Orientation.Vertical),
                                RequirementDictionary.Instance[RequirementType.DungeonItemShuffleKeysanity]
                            })));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                    }
                    break;
                case LocationID.EasternPalace:
                    {
                        smallItems.Add(GetSpacerSmallItemControlVM(
                            RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.EPBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.EPBoss));
                    }
                    break;
                case LocationID.DesertPalace:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.DPSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.DPBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.DPBoss));
                    }
                    break;
                case LocationID.TowerOfHera:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.ToHSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.ToHBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.ToHBoss));
                    }
                    break;
                case LocationID.PalaceOfDarkness:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.PoDSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.PoDBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.PoDBoss));
                    }
                    break;
                case LocationID.SwampPalace:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.SPSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.SPBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.SPBoss));
                    }
                    break;
                case LocationID.SkullWoods:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.SWSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.SWBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.SWBoss));
                    }
                    break;
                case LocationID.ThievesTown:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.TTSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.TTBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.TTBoss));
                    }
                    break;
                case LocationID.IcePalace:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.IPSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.IPBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.IPBoss));
                    }
                    break;
                case LocationID.MiseryMire:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.MMSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.MMBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.MMBoss));
                    }
                    break;
                case LocationID.TurtleRock:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.TRSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.TRBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetPrizeSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.TRBoss));
                    }
                    break;
                case LocationID.GanonsTower:
                    {
                        smallItems.Add(GetSmallKeySmallItemControlVM(ItemType.GTSmallKey));
                        smallItems.Add(GetBigKeySmallItemControlVM(ItemType.GTBigKey));
                        smallItems.Add(GetDungeonItemSmallItemControlVM(location));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.GTBoss1));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.GTBoss2));
                        smallItems.Add(GetBossSmallItemControlVM(BossPlacementID.GTBoss3));
                    }
                    break;
                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(location));
                    }
            }

            return smallItems;
        }
    }
}
