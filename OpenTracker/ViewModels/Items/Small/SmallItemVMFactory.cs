using Avalonia.Layout;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using System.Collections.Generic;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the class containing creation logic for small item control ViewModel classes.
    /// </summary>
    public class SmallItemVMFactory : ISmallItemVMFactory
    {
        private readonly ILocationDictionary _locations;
        private readonly IRequirementDictionary _requirements;

        private readonly AggregateRequirement.Factory _aggregateFactory;
        private readonly AlternativeRequirement.Factory _alternativeFactory;
        private readonly AlwaysDisplayDungeonItemsRequirement.Factory _alwaysDisplayFactory;
        private readonly DisplayMapsCompassesRequirement.Factory _displayMapsCompassesFactory;
        private readonly ItemsPanelOrientationRequirement.Factory _itemsPanelOrientationFactory;

        private readonly SmallKeySmallItemVM.Factory _smallKeyFactory;
        private readonly SpacerSmallItemVM.Factory _spacerFactory;
        private readonly SmallItemVM.Factory _smallItemFactory;
        private readonly BigKeySmallItemVM.Factory _bigKeyFactory;
        private readonly DungeonItemSmallItemVM.Factory _dungeonItemFactory;
        private readonly PrizeSmallItemVM.Factory _prizeFactory;
        private readonly BossSmallItemVM.Factory _bossFactory;

        public SmallItemVMFactory(
            ILocationDictionary locations, IRequirementDictionary requirements,
            AggregateRequirement.Factory aggregateFactory,
            AlternativeRequirement.Factory alternativeFactory,
            AlwaysDisplayDungeonItemsRequirement.Factory alwaysDisplayFactory,
            DisplayMapsCompassesRequirement.Factory displayMapsCompassesFactory,
            ItemsPanelOrientationRequirement.Factory itemsPanelOrientationFactory,
            SmallKeySmallItemVM.Factory smallKeyFactory,
            SpacerSmallItemVM.Factory spacerFactory,
            SmallItemVM.Factory smallItemFactory, BigKeySmallItemVM.Factory bigKeyFactory,
            DungeonItemSmallItemVM.Factory dungeonItemFactory,
            PrizeSmallItemVM.Factory prizeFactory, BossSmallItemVM.Factory bossFactory)
        {
            _locations = locations;
            _requirements = requirements;

            _aggregateFactory = aggregateFactory;
            _alternativeFactory = alternativeFactory;
            _alwaysDisplayFactory = alwaysDisplayFactory;
            _displayMapsCompassesFactory = displayMapsCompassesFactory;
            _itemsPanelOrientationFactory = itemsPanelOrientationFactory;

            _smallKeyFactory = smallKeyFactory;
            _spacerFactory = spacerFactory;
            _smallItemFactory = smallItemFactory;
            _bigKeyFactory = bigKeyFactory;
            _dungeonItemFactory = dungeonItemFactory;
            _prizeFactory = prizeFactory;
            _bossFactory = bossFactory;
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
        private SmallItemVM GetCompassSmallItemControlVM(IDungeon dungeon)
        {
            return _smallItemFactory(
                dungeon.CompassItem!,
                _aggregateFactory(new List<IRequirement>
                {
                    _displayMapsCompassesFactory(true),
                    _alternativeFactory(new List<IRequirement>
                    {
                        _alwaysDisplayFactory(true),
                        _requirements[RequirementType.CompassShuffleOn]
                    })
                }),
                "avares://OpenTracker/Assets/Images/Items/compass");
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
        private SmallItemVM GetMapSmallItemControlVM(IDungeon dungeon)
        {
            return _smallItemFactory(
                dungeon.MapItem!,
                _aggregateFactory(new List<IRequirement>
                {
                    _displayMapsCompassesFactory(true),
                    _alternativeFactory(new List<IRequirement>
                    {
                        _alwaysDisplayFactory(true),
                        _requirements[RequirementType.MapShuffleOn]
                    })
                }),
                "avares://OpenTracker/Assets/Images/Items/map");
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
        private BigKeySmallItemVM GetBigKeySmallItemControlVM(IDungeon dungeon)
        {
            IRequirement requirement;
            IRequirement spacerRequirement;

            if (dungeon.ID == LocationID.HyruleCastle)
            {
                requirement = _aggregateFactory(new List<IRequirement>
                {
                    _requirements[RequirementType.KeyDropShuffleOn],
                    _alternativeFactory(new List<IRequirement>
                    {
                        _alwaysDisplayFactory(true),
                        _requirements[RequirementType.BigKeyShuffleOn]
                    })
                });
                spacerRequirement = _alternativeFactory(new List<IRequirement>
                {
                    _aggregateFactory(new List<IRequirement>
                    {
                        _itemsPanelOrientationFactory(Orientation.Vertical),
                        _alternativeFactory(new List<IRequirement>
                        {
                            _alwaysDisplayFactory(true),
                            _requirements[RequirementType.BigKeyShuffleOn]
                        })
                    }),
                    _aggregateFactory(new List<IRequirement>
                    {
                        _requirements[RequirementType.KeyDropShuffleOn],
                        _alternativeFactory(new List<IRequirement>
                        {
                            _alwaysDisplayFactory(true),
                            _requirements[RequirementType.BigKeyShuffleOn]
                        })
                    })
                });
            }
            else
            {
                spacerRequirement = _alternativeFactory(new List<IRequirement>
                {
                    _alwaysDisplayFactory(true),
                    _requirements[RequirementType.BigKeyShuffleOn]
                });
                requirement = _alternativeFactory(new List<IRequirement>
                {
                    _alwaysDisplayFactory(true),
                    _requirements[RequirementType.BigKeyShuffleOn]
                });
            }

            return _bigKeyFactory(dungeon, requirement, spacerRequirement);
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
        public List<ISmallItemVMBase> GetSmallItemControlVMs(LocationID location)
        {
            var smallItems = new List<ISmallItemVMBase>();
            var dungeon = (IDungeon)_locations[location];

            if (dungeon.CompassItem == null)
            {
                smallItems.Add(_spacerFactory(
                    _aggregateFactory(new List<IRequirement>
                    {
                        _displayMapsCompassesFactory(true),
                        _itemsPanelOrientationFactory(Orientation.Vertical),
                        _alternativeFactory(new List<IRequirement>
                        {
                            _alwaysDisplayFactory(true),
                            _requirements[RequirementType.CompassShuffleOn]
                        })
                    })));
            }
            else
            {
                smallItems.Add(GetCompassSmallItemControlVM(dungeon));
            }

            if (dungeon.MapItem == null)
            {
                smallItems.Add(_spacerFactory(
                    _aggregateFactory(new List<IRequirement>
                    {
                        _displayMapsCompassesFactory(true),
                        _itemsPanelOrientationFactory(Orientation.Vertical),
                        _alternativeFactory(new List<IRequirement>
                        {
                            _alwaysDisplayFactory(true),
                            _requirements[RequirementType.MapShuffleOn]
                        })
                    })));
            }
            else
            {
                smallItems.Add(GetMapSmallItemControlVM(dungeon));
            }
            
            smallItems.Add(_smallKeyFactory(dungeon));

            if (dungeon.BigKeyItem == null)
            {
                smallItems.Add(_spacerFactory(
                    _aggregateFactory(new List<IRequirement>
                    {
                        _itemsPanelOrientationFactory(Orientation.Vertical),
                        _alternativeFactory(new List<IRequirement>
                        {
                            _alwaysDisplayFactory(true),
                            _requirements[RequirementType.BigKeyShuffleOn]
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
                    smallItems.Add(_dungeonItemFactory(section));
                }

                if (section is IPrizeSection prizeSection && location != LocationID.AgahnimTower &&
                    location != LocationID.GanonsTower)
                {
                    smallItems.Add(_prizeFactory(prizeSection));
                }

                if (section is IBossSection bossSection &&
                    bossSection.BossPlacement.Boss != BossType.Aga)
                {
                    smallItems.Add(_bossFactory(bossSection.BossPlacement));
                }
            }

            return smallItems;
        }
    }
}
