using System.Collections.Generic;
using Avalonia.Layout;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using OpenTracker.ViewModels.Items;
using OpenTracker.ViewModels.Items.Adapters;

namespace OpenTracker.ViewModels.Dungeons
{
    public class DungeonVMFactory : IDungeonVMFactory
    {
        private readonly ILocationDictionary _locations;
        private readonly IRequirementDictionary _requirements;

        private readonly AggregateRequirement.Factory _aggregateFactory;
        private readonly AlternativeRequirement.Factory _alternativeFactory;
        private readonly AlwaysDisplayDungeonItemsRequirement.Factory _alwaysDisplayFactory;
        private readonly DisplayMapsCompassesRequirement.Factory _displayMapsCompassesFactory;
        private readonly ItemsPanelOrientationRequirement.Factory _itemsPanelOrientationFactory;

        private readonly IDungeonItemVM.Factory _factory;
        private readonly IItemVM.Factory _itemFactory;

        private readonly ItemAdapter.Factory _adapterFactory;
        private readonly DungeonSmallKeyAdapter.Factory _smallKeyFactory;
        private readonly IDungeonItemSectionVM.Factory _dungeonItemFactory;
        private readonly PrizeAdapter.Factory _prizeFactory;
        private readonly BossAdapter.Factory _bossFactory;

        public DungeonVMFactory(
            ILocationDictionary locations, IRequirementDictionary requirements,
            AggregateRequirement.Factory aggregateFactory, AlternativeRequirement.Factory alternativeFactory,
            AlwaysDisplayDungeonItemsRequirement.Factory alwaysDisplayFactory,
            DisplayMapsCompassesRequirement.Factory displayMapsCompassesFactory,
            ItemsPanelOrientationRequirement.Factory itemsPanelOrientationFactory,
            IDungeonItemVM.Factory factory, IItemVM.Factory itemFactory, ItemAdapter.Factory adapterFactory,
            DungeonSmallKeyAdapter.Factory smallKeyFactory, IDungeonItemSectionVM.Factory dungeonItemFactory,
            PrizeAdapter.Factory prizeFactory, BossAdapter.Factory bossFactory)
        {
            _locations = locations;
            _requirements = requirements;

            _aggregateFactory = aggregateFactory;
            _alternativeFactory = alternativeFactory;
            _alwaysDisplayFactory = alwaysDisplayFactory;
            _displayMapsCompassesFactory = displayMapsCompassesFactory;
            _itemsPanelOrientationFactory = itemsPanelOrientationFactory;

            _factory = factory;
            _itemFactory = itemFactory;

            _adapterFactory = adapterFactory;
            _smallKeyFactory = smallKeyFactory;
            _dungeonItemFactory = dungeonItemFactory;
            _prizeFactory = prizeFactory;
            _bossFactory = bossFactory;
        }

        /// <summary>
        /// Returns a new small item control representing a compass.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon from which the compass represented.
        /// </param>
        /// <returns>
        /// A new small item control.
        /// </returns>
        private IDungeonItemVM GetCompassItemVM(IDungeon dungeon)
        {
            var compassItem = dungeon.CompassItem;

            var requirement = compassItem is null ?
                (IRequirement) _aggregateFactory(new List<IRequirement>
                {
                    _displayMapsCompassesFactory(true),
                    _itemsPanelOrientationFactory(Orientation.Vertical),
                    _alternativeFactory(new List<IRequirement>
                    {
                        _alwaysDisplayFactory(true),
                        _requirements[RequirementType.CompassShuffleOn]
                    })
                })
                : _aggregateFactory(new List<IRequirement>
                {
                    _displayMapsCompassesFactory(true),
                    _alternativeFactory(new List<IRequirement>
                    {
                        _alwaysDisplayFactory(true),
                        _requirements[RequirementType.CompassShuffleOn]
                    })
                }); 

            var item = compassItem is null ? null : _itemFactory(
                _adapterFactory(compassItem, "avares://OpenTracker/Assets/Images/Items/compass"),
                _requirements[RequirementType.NoRequirement]);

            return _factory(requirement, item);
        }

        /// <summary>
        /// Returns a new small item control representing a map.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon from which the map represented.
        /// </param>
        /// <returns>
        /// A new small item control.
        /// </returns>
        private IDungeonItemVM GetMapItemVM(IDungeon dungeon)
        {
            var mapItem = dungeon.MapItem;

            var requirement = mapItem is null ?
                (IRequirement) _aggregateFactory(new List<IRequirement>
                {
                    _displayMapsCompassesFactory(true),
                    _itemsPanelOrientationFactory(Orientation.Vertical),
                    _alternativeFactory(new List<IRequirement>
                    {
                        _alwaysDisplayFactory(true),
                        _requirements[RequirementType.MapShuffleOn]
                    })
                })
                : _aggregateFactory(new List<IRequirement>
                {
                    _displayMapsCompassesFactory(true),
                    _alternativeFactory(new List<IRequirement>
                    {
                        _alwaysDisplayFactory(true),
                        _requirements[RequirementType.MapShuffleOn]
                    })
                });

            var item = mapItem is null ? null : _itemFactory(
                _adapterFactory(mapItem, "avares://OpenTracker/Assets/Images/Items/map"),
                _requirements[RequirementType.NoRequirement]);

            return _factory(requirement, item);
        }

        /// <summary>
        /// Returns a new small item control representing a small key.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon from which the small key represented.
        /// </param>
        /// <returns>
        /// A new small item control.
        /// </returns>
        private IDungeonItemVM GetSmallKeyItemVM(IDungeon dungeon)
        {
            var requirement = dungeon.ID == LocationID.EasternPalace ?
                _requirements[RequirementType.KeyDropShuffleOn] :
                _requirements[RequirementType.NoRequirement];
            
            var spacerRequirement = _alternativeFactory(new List<IRequirement>
            {
                _alwaysDisplayFactory(true),
                _requirements[RequirementType.SmallKeyShuffleOn]
            });

            var item = _itemFactory(_smallKeyFactory(dungeon.SmallKeyItem), requirement);

            return _factory(spacerRequirement, item);
        }

        /// <summary>
        /// Returns a new small item control representing a big key.
        /// </summary>
        /// <param name="dungeon">
        /// The dungeon from which the big key represented.
        /// </param>
        /// <returns>
        /// A new small item control.
        /// </returns>
        private IDungeonItemVM GetBigKeyItemVM(IDungeon dungeon)
        {
            var bigKeyItem = dungeon.BigKeyItem;

            var requirement = dungeon.ID == LocationID.HyruleCastle ?
                (IRequirement) _aggregateFactory(new List<IRequirement>
                {
                    _requirements[RequirementType.KeyDropShuffleOn],
                    _alternativeFactory(new List<IRequirement>
                    {
                        _alwaysDisplayFactory(true),
                        _requirements[RequirementType.BigKeyShuffleOn]
                    })
                }) :
                _alternativeFactory(new List<IRequirement>
                {
                    _alwaysDisplayFactory(true),
                    _requirements[RequirementType.BigKeyShuffleOn]
                });
            var spacerRequirement = bigKeyItem is null ?
                _aggregateFactory(new List<IRequirement>
                {
                    _itemsPanelOrientationFactory(Orientation.Vertical),
                    _alternativeFactory(new List<IRequirement>
                    {
                        _alwaysDisplayFactory(true),
                        _requirements[RequirementType.BigKeyShuffleOn]
                        
                    })
                }) : dungeon.ID == LocationID.HyruleCastle ?
                (IRequirement) _alternativeFactory(new List<IRequirement>
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
                }) :
                _alternativeFactory(new List<IRequirement>
                {
                    _alwaysDisplayFactory(true),
                    _requirements[RequirementType.BigKeyShuffleOn]
                });
            
            var item = bigKeyItem is null ? null : _itemFactory(_adapterFactory(
                bigKeyItem, "avares://OpenTracker/Assets/Images/Items/bigkey"), requirement);

            return _factory(spacerRequirement, item);
        }

        public List<IDungeonItemVM> GetDungeonItemVMs(LocationID id)
        {
            var dungeon = (IDungeon)_locations[id];

            var dungeonItems = new List<IDungeonItemVM>
            {
                GetCompassItemVM(dungeon),
                GetMapItemVM(dungeon),
                GetSmallKeyItemVM(dungeon),
                GetBigKeyItemVM(dungeon)
            };

            foreach (var section in dungeon.Sections)
            {
                switch (section)
                {
                    case IDungeonItemSection _:
                        dungeonItems.Add(_dungeonItemFactory(section));
                        break;
                    case IPrizeSection prizeSection when id != LocationID.AgahnimTower && id != LocationID.GanonsTower:
                        dungeonItems.Add(_factory(
                            _requirements[RequirementType.NoRequirement],
                            _itemFactory(
                                _prizeFactory(prizeSection), _requirements[RequirementType.NoRequirement])));
                        break;
                    case IBossSection bossSection when bossSection.BossPlacement.Boss != BossType.Aga:
                        dungeonItems.Add(_factory(
                            _requirements[RequirementType.BossShuffleOn],
                            _itemFactory(
                                _bossFactory(bossSection.BossPlacement),
                                _requirements[RequirementType.NoRequirement])));
                        break;
                }
            }

            return dungeonItems;
        }
    }
}