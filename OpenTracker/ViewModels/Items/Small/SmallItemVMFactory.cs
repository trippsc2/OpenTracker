using Avalonia.Layout;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons;
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
        /// <param name="dungeon">
        /// The dungeon from which the small key represented.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static SmallKeySmallItemVM GetSmallKeySmallItemControlVM(IDungeon dungeon)
        {
            return new SmallKeySmallItemVM(dungeon);
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a spacer.
        /// </summary>
        /// <param name="requirement">
        /// The requirement for the spacer to be visible.
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
        /// <param name="section">
        /// The section to be represented.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static DungeonItemSmallItemVM GetDungeonItemSmallItemControlVM(
            ISection section)
        {
            return new DungeonItemSmallItemVM(section);
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a dungeon prize.
        /// </summary>
        /// <param name="section">
        /// The prize section.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static PrizeSmallItemVM GetPrizeSmallItemControlVM(IPrizeSection section)
        {
            return new PrizeSmallItemVM(section);
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a compass.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon from which the compass represented.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static SmallItemVM GetCompassSmallItemControlVM(IDungeon dungeon)
        {
            return new SmallItemVM(
                "avares://OpenTracker/Assets/Images/Items/compass", dungeon.CompassItem,
                new AggregateRequirement(new List<IRequirement>
                {
                    new DisplayMapsCompassesRequirement(true),
                    new AlternativeRequirement(new List<IRequirement>
                    {
                        new AlwaysDisplayDungeonItemsRequirement(true),
                        RequirementDictionary.Instance[RequirementType.CompassShuffleOn]
                    })
                }));
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a map.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon from which the map represented.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static SmallItemVM GetMapSmallItemControlVM(IDungeon dungeon)
        {
            return new SmallItemVM(
                "avares://OpenTracker/Assets/Images/Items/map", dungeon.MapItem,
                new AggregateRequirement(new List<IRequirement>
                {
                    new DisplayMapsCompassesRequirement(true),
                    new AlternativeRequirement(new List<IRequirement>
                    {
                        new AlwaysDisplayDungeonItemsRequirement(true),
                        RequirementDictionary.Instance[RequirementType.MapShuffleOn]
                    })
                }));
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a big key.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon from which the big key represented.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static SmallItemVM GetBigKeySmallItemControlVM(IDungeon dungeon)
        {
            return new SmallItemVM(
                "avares://OpenTracker/Assets/Images/Items/bigkey", dungeon.BigKeyItem,
                new AlternativeRequirement(new List<IRequirement>
                {
                    new AlwaysDisplayDungeonItemsRequirement(true),
                    RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]
                }));
        }

        /// <summary>
        /// Returns a new small item control ViewModel instance representing a boss.
        /// </summary>
        /// <param name="bossPlacement">
        /// The boss placement ID to be represented.
        /// </param>
        /// <returns>
        /// A new small item control ViewModel instance.
        /// </returns>
        private static BossSmallItemVM GetBossSmallItemControlVM(IBossPlacement bossPlacement)
        {
            return new BossSmallItemVM(bossPlacement);
        }

        /// <summary>
        /// Returns a new observable collection of small item control ViewModel instances for the
        /// specified location.
        /// </summary>
        /// <param name="location">
        /// The location ID.
        /// </param>
        /// <returns>
        /// A new observable collection of small item control ViewModel instances.
        /// </returns>
        internal static ObservableCollection<SmallItemVMBase> GetSmallItemControlVMs(
            LocationID location)
        {
            var smallItems = new ObservableCollection<SmallItemVMBase>();
            var dungeon = (IDungeon)LocationDictionary.Instance[location];

            if (dungeon.CompassItem == null)
            {
                smallItems.Add(GetSpacerSmallItemControlVM(
                    new AggregateRequirement(new List<IRequirement>
                    {
                        new DisplayMapsCompassesRequirement(true),
                        new ItemsPanelOrientationRequirement(Orientation.Vertical),
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AlwaysDisplayDungeonItemsRequirement(true),
                            RequirementDictionary.Instance[RequirementType.CompassShuffleOn]
                        })
                    })));
            }
            else
            {
                smallItems.Add(GetCompassSmallItemControlVM(dungeon));
            }

            if (dungeon.MapItem == null)
            {
                smallItems.Add(GetSpacerSmallItemControlVM(
                    new AggregateRequirement(new List<IRequirement>
                    {
                        new DisplayMapsCompassesRequirement(true),
                        new ItemsPanelOrientationRequirement(Orientation.Vertical),
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AlwaysDisplayDungeonItemsRequirement(true),
                            RequirementDictionary.Instance[RequirementType.MapShuffleOn]
                        })
                    })));
            }
            else
            {
                smallItems.Add(GetMapSmallItemControlVM(dungeon));
            }

            if (dungeon.SmallKeyItem == null)
            {
                smallItems.Add(GetSpacerSmallItemControlVM(
                    new AlternativeRequirement(new List<IRequirement>
                    {
                        new AlwaysDisplayDungeonItemsRequirement(true),
                        RequirementDictionary.Instance[RequirementType.SmallKeyShuffleOn]
                    })));
            }
            else
            {
                smallItems.Add(GetSmallKeySmallItemControlVM(dungeon));
            }

            if (dungeon.BigKeyItem == null)
            {
                smallItems.Add(GetSpacerSmallItemControlVM(
                    new AggregateRequirement(new List<IRequirement>
                    {
                        new ItemsPanelOrientationRequirement(Orientation.Vertical),
                        new AlternativeRequirement(new List<IRequirement>
                        {
                            new AlwaysDisplayDungeonItemsRequirement(true),
                            RequirementDictionary.Instance[RequirementType.BigKeyShuffleOn]
                        })
                    })));
            }
            else
            {
                smallItems.Add(GetBigKeySmallItemControlVM(dungeon));
            }

            foreach (var section in dungeon.Sections)
            {
                if (section is IDungeonItemSection)
                {
                    smallItems.Add(GetDungeonItemSmallItemControlVM(section));
                }

                if (section is IPrizeSection prizeSection && location != LocationID.AgahnimTower &&
                    location != LocationID.GanonsTower)
                {
                    smallItems.Add(GetPrizeSmallItemControlVM(prizeSection));
                }

                if (section is IBossSection bossSection &&
                    bossSection.BossPlacement.Boss != BossType.Aga)
                {
                    smallItems.Add(GetBossSmallItemControlVM(bossSection.BossPlacement));
                }
            }

            return smallItems;
        }
    }
}
