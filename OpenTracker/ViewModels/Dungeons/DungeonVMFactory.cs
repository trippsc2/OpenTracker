using System;
using System.Collections.Generic;
using Avalonia.Layout;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.AlwaysDisplay;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.BossShuffle;
using OpenTracker.Models.Requirements.CompassShuffle;
using OpenTracker.Models.Requirements.DisplaysMapsCompasses;
using OpenTracker.Models.Requirements.ItemsPanelOrientation;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.MapShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Models.Sections;
using OpenTracker.Models.Sections.Boss;
using OpenTracker.Models.Sections.Item;
using OpenTracker.ViewModels.Items;
using OpenTracker.ViewModels.Items.Adapters;

namespace OpenTracker.ViewModels.Dungeons
{
    public class DungeonVMFactory : IDungeonVMFactory
    {
        private readonly IDungeonDictionary _dungeons;
        private readonly ILocationDictionary _locations;

        private readonly IAggregateRequirementDictionary _aggregateRequirements;
        private readonly IAlternativeRequirementDictionary _alternativeRequirements;
        private readonly IAlwaysDisplayDungeonItemsRequirementDictionary _alwaysDisplayDungeonItemsRequirements;
        private readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements;
        private readonly IBossShuffleRequirementDictionary _bossShuffleRequirements;
        private readonly ICompassShuffleRequirementDictionary _compassShuffleRequirements;
        private readonly IDisplayMapsCompassesRequirementDictionary _displayMapsCompassesRequirements;
        private readonly IItemsPanelOrientationRequirementDictionary _itemsPanelOrientationRequirements;
        private readonly IKeyDropShuffleRequirementDictionary _keyDropShuffleRequirements;
        private readonly IMapShuffleRequirementDictionary _mapShuffleRequirements;
        private readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements;

        private readonly IDungeonItemVM.Factory _factory;
        private readonly IItemVM.Factory _itemFactory;

        private readonly ItemAdapter.Factory _adapterFactory;
        private readonly DungeonSmallKeyAdapter.Factory _smallKeyFactory;
        private readonly IDungeonItemSectionVM.Factory _dungeonItemFactory;
        private readonly PrizeAdapter.Factory _prizeFactory;
        private readonly BossAdapter.Factory _bossFactory;

        public DungeonVMFactory(
            IDungeonDictionary dungeons, ILocationDictionary locations,
            IAggregateRequirementDictionary aggregateRequirements,
            IAlternativeRequirementDictionary alternativeRequirements,
            IAlwaysDisplayDungeonItemsRequirementDictionary alwaysDisplayDungeonItemsRequirements,
            IBigKeyShuffleRequirementDictionary bigKeyShuffleRequirements,
            IBossShuffleRequirementDictionary bossShuffleRequirements,
            ICompassShuffleRequirementDictionary compassShuffleRequirements,
            IDisplayMapsCompassesRequirementDictionary displayMapsCompassesRequirements,
            IItemsPanelOrientationRequirementDictionary itemsPanelOrientationRequirements,
            IKeyDropShuffleRequirementDictionary keyDropShuffleRequirements,
            IMapShuffleRequirementDictionary mapShuffleRequirements,
            ISmallKeyShuffleRequirementDictionary smallKeyShuffleRequirements, IDungeonItemVM.Factory factory,
            IItemVM.Factory itemFactory, ItemAdapter.Factory adapterFactory,
            DungeonSmallKeyAdapter.Factory smallKeyFactory, IDungeonItemSectionVM.Factory dungeonItemFactory,
            PrizeAdapter.Factory prizeFactory, BossAdapter.Factory bossFactory)
        {
            _locations = locations;

            _factory = factory;
            _itemFactory = itemFactory;

            _adapterFactory = adapterFactory;
            _smallKeyFactory = smallKeyFactory;
            _dungeonItemFactory = dungeonItemFactory;
            _prizeFactory = prizeFactory;
            _bossFactory = bossFactory;
            _aggregateRequirements = aggregateRequirements;
            _alternativeRequirements = alternativeRequirements;
            _alwaysDisplayDungeonItemsRequirements = alwaysDisplayDungeonItemsRequirements;
            _bigKeyShuffleRequirements = bigKeyShuffleRequirements;
            _bossShuffleRequirements = bossShuffleRequirements;
            _compassShuffleRequirements = compassShuffleRequirements;
            _displayMapsCompassesRequirements = displayMapsCompassesRequirements;
            _itemsPanelOrientationRequirements = itemsPanelOrientationRequirements;
            _keyDropShuffleRequirements = keyDropShuffleRequirements;
            _mapShuffleRequirements = mapShuffleRequirements;
            _smallKeyShuffleRequirements = smallKeyShuffleRequirements;
            _dungeons = dungeons;
        }

        /// <summary>
        ///     Returns a new small item control representing a compass.
        /// </summary>
        /// <param name="dungeon">
        ///     The dungeon from which the compass represented.
        /// </param>
        /// <returns>
        ///     A new small item control.
        /// </returns>
        private IDungeonItemVM GetCompassItemVM(IDungeon dungeon)
        {
            var compassItem = dungeon.Compass;

            var requirement = compassItem is null
                ? _aggregateRequirements[new HashSet<IRequirement>
                {
                    _displayMapsCompassesRequirements[true],
                    _itemsPanelOrientationRequirements[Orientation.Vertical],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _alwaysDisplayDungeonItemsRequirements[true],
                        _compassShuffleRequirements[true]
                    }]
                }]
                : _aggregateRequirements[new HashSet<IRequirement>
                {
                    _displayMapsCompassesRequirements[true],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _alwaysDisplayDungeonItemsRequirements[true],
                        _compassShuffleRequirements[true]
                    }]
                }]; 

            var item = compassItem is null ? null : _itemFactory(
                _adapterFactory(compassItem, "avares://OpenTracker/Assets/Images/Items/compass"));

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
            var mapItem = dungeon.Map;

            var requirement = mapItem is null
                ? _aggregateRequirements[new HashSet<IRequirement>
                {
                    _displayMapsCompassesRequirements[true],
                    _itemsPanelOrientationRequirements[Orientation.Vertical],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _alwaysDisplayDungeonItemsRequirements[true],
                        _mapShuffleRequirements[true]
                    }]
                }]
                : _aggregateRequirements[new HashSet<IRequirement>
                {
                    _displayMapsCompassesRequirements[true],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _alwaysDisplayDungeonItemsRequirements[true],
                        _mapShuffleRequirements[true]
                    }]
                }];

            var item = mapItem is null ? null : _itemFactory(
                _adapterFactory(mapItem, "avares://OpenTracker/Assets/Images/Items/map"));

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
            var requirement = dungeon.ID == DungeonID.EasternPalace 
                ? _keyDropShuffleRequirements[true] : null;
            
            var spacerRequirement = _alternativeRequirements[new HashSet<IRequirement>
            {
                _alwaysDisplayDungeonItemsRequirements[true],
                _smallKeyShuffleRequirements[true]
            }];

            var item = _itemFactory(_smallKeyFactory(dungeon.SmallKey), requirement);

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
            var bigKeyItem = dungeon.BigKey;

            var requirement = dungeon.ID == DungeonID.HyruleCastle
                ? _aggregateRequirements[new HashSet<IRequirement>
                {
                    _keyDropShuffleRequirements[true],
                    _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _alwaysDisplayDungeonItemsRequirements[true],
                        _bigKeyShuffleRequirements[true]
                    }]
                }]
                : _alternativeRequirements[new HashSet<IRequirement>
                {
                    _alwaysDisplayDungeonItemsRequirements[true],
                    _bigKeyShuffleRequirements[true]
                }];
            var spacerRequirement = bigKeyItem is null ?
                _aggregateRequirements[new HashSet<IRequirement>
                {
                    _itemsPanelOrientationRequirements[Orientation.Vertical],
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _alwaysDisplayDungeonItemsRequirements[true],
                        _bigKeyShuffleRequirements[true]
                        
                    }]
                }]
                : dungeon.ID == DungeonID.HyruleCastle
                    ? _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _itemsPanelOrientationRequirements[Orientation.Vertical],
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _alwaysDisplayDungeonItemsRequirements[true],
                                _bigKeyShuffleRequirements[true]
                            }]
                        }],
                        _aggregateRequirements[new HashSet<IRequirement>
                        {
                            _keyDropShuffleRequirements[true],
                            _alternativeRequirements[new HashSet<IRequirement>
                            {
                                _alwaysDisplayDungeonItemsRequirements[true],
                                _bigKeyShuffleRequirements[true]
                            }]
                        }]
                    }]
                    : _alternativeRequirements[new HashSet<IRequirement>
                    {
                        _alwaysDisplayDungeonItemsRequirements[true],
                        _bigKeyShuffleRequirements[true]
                    }];
            
            var item = bigKeyItem is null ? null : _itemFactory(_adapterFactory(
                bigKeyItem, "avares://OpenTracker/Assets/Images/Items/bigkey"), requirement);

            return _factory(spacerRequirement, item);
        }

        public List<IDungeonItemVM> GetDungeonItemVMs(LocationID id)
        {
            var location = _locations[id];
            var dungeonID = Enum.Parse<DungeonID>(id.ToString());
            var dungeon = _dungeons[dungeonID];

            var dungeonItems = new List<IDungeonItemVM>
            {
                GetCompassItemVM(dungeon),
                GetMapItemVM(dungeon),
                GetSmallKeyItemVM(dungeon),
                GetBigKeyItemVM(dungeon)
            };

            foreach (var section in location.Sections)
            {
                switch (section)
                {
                    case IDungeonItemSection _:
                        dungeonItems.Add(_dungeonItemFactory(section));
                        break;
                    case IPrizeSection prizeSection when id != LocationID.AgahnimTower && id != LocationID.GanonsTower:
                        dungeonItems.Add(_factory(null, _itemFactory(_prizeFactory(prizeSection))));
                        break;
                    case IBossSection bossSection when bossSection.BossPlacement.Boss != BossType.Aga:
                        dungeonItems.Add(_factory(
                            _bossShuffleRequirements[true], _itemFactory(_bossFactory(bossSection.BossPlacement))));
                        break;
                }
            }

            return dungeonItems;
        }
    }
}