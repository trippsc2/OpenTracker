using System.Collections.Generic;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.KeyLayouts.Factories;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories
{
    public class SWKeyLayoutFactoryTests
    {
        private static readonly IAggregateRequirementDictionary _aggregateRequirements =
            new AggregateRequirementDictionary(requirements =>
                new AggregateRequirement(requirements));
        private static readonly IBigKeyShuffleRequirementDictionary _bigKeyShuffleRequirements =
            Substitute.For<IBigKeyShuffleRequirementDictionary>();
        private static readonly IGuaranteedBossItemsRequirementDictionary _guaranteedBossItemsRequirements =
            Substitute.For<IGuaranteedBossItemsRequirementDictionary>();
        private static readonly IItemPlacementRequirementDictionary _itemPlacementRequirements =
            Substitute.For<IItemPlacementRequirementDictionary>();
        private static readonly IKeyDropShuffleRequirementDictionary _keyDropShuffleRequirements =
            Substitute.For<IKeyDropShuffleRequirementDictionary>();
        private static readonly ISmallKeyShuffleRequirementDictionary _smallKeyShuffleRequirements =
            Substitute.For<ISmallKeyShuffleRequirementDictionary>();

        private static readonly IBigKeyLayout.Factory _bigKeyFactory = (bigKeyLocations, children, requirement) =>
            new BigKeyLayout(bigKeyLocations, children, requirement);
        private static readonly IEndKeyLayout.Factory _endFactory = requirement => new EndKeyLayout(requirement);
        private static readonly ISmallKeyLayout.Factory _smallKeyFactory =
            (count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement) => new SmallKeyLayout(
                count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement);

        private readonly SWKeyLayoutFactory _sut = new(
            _aggregateRequirements, _bigKeyShuffleRequirements, _guaranteedBossItemsRequirements,
            _itemPlacementRequirements, _keyDropShuffleRequirements,
            _smallKeyShuffleRequirements, _bigKeyFactory, _endFactory,
            _smallKeyFactory);

        [Fact]
        public void GetDungeonKeyLayouts_ShouldReturnExpected()
        {
            var dungeon = Substitute.For<IDungeon>();
            
            var expected = (new List<IKeyLayout>
            {
                _endFactory(_aggregateRequirements[new HashSet<IRequirement>
                {
                    _bigKeyShuffleRequirements[true],
                    _smallKeyShuffleRequirements[true]
                }]),
                _smallKeyFactory(
                    3, new List<DungeonItemID>
                    {
                        DungeonItemID.SWBigKeyChest,
                        DungeonItemID.SWMapChest,
                        DungeonItemID.SWBigChest,
                        DungeonItemID.SWPotPrison,
                        DungeonItemID.SWCompassChest,
                        DungeonItemID.SWPinballRoom,
                        DungeonItemID.SWBridgeRoom
                    },
                    false, new List<IKeyLayout> {_endFactory()}, dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _keyDropShuffleRequirements[false],
                    }]),
                _smallKeyFactory(
                    1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                    new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWBigChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    dungeon, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _itemPlacementRequirements[ItemPlacement.Basic],
                        _keyDropShuffleRequirements[false]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.SWBigKeyChest,
                        DungeonItemID.SWMapChest,
                        DungeonItemID.SWBigChest,
                        DungeonItemID.SWPotPrison,
                        DungeonItemID.SWCompassChest,
                        DungeonItemID.SWPinballRoom,
                        DungeonItemID.SWBridgeRoom
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWBigChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom
                            },
                            true, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _keyDropShuffleRequirements[false]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.SWBigKeyChest,
                        DungeonItemID.SWMapChest,
                        DungeonItemID.SWBigChest,
                        DungeonItemID.SWPotPrison,
                        DungeonItemID.SWCompassChest,
                        DungeonItemID.SWBridgeRoom
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    3, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWBigChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom
                                    },
                                    true, new List<IKeyLayout> {_endFactory()}, dungeon)
                            }, dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _itemPlacementRequirements[ItemPlacement.Basic],
                        _keyDropShuffleRequirements[false]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.SWBoss}, new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            3, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon,
                            _itemPlacementRequirements[ItemPlacement.Advanced]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    3, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon, _itemPlacementRequirements[ItemPlacement.Basic])
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _guaranteedBossItemsRequirements[false],
                        _keyDropShuffleRequirements[false]
                    }]),
                _smallKeyFactory(
                    4, new List<DungeonItemID>
                    {
                        DungeonItemID.SWBigKeyChest,
                        DungeonItemID.SWMapChest,
                        DungeonItemID.SWBigChest,
                        DungeonItemID.SWPotPrison,
                        DungeonItemID.SWCompassChest,
                        DungeonItemID.SWPinballRoom,
                        DungeonItemID.SWBridgeRoom,
                        DungeonItemID.SWWestLobbyPot
                    },
                    false, new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            5, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWBigChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom,
                                DungeonItemID.SWWestLobbyPot,
                                DungeonItemID.SWSpikeCornerDrop
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                    },
                    dungeon,
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _keyDropShuffleRequirements[true]
                    }]),
                _smallKeyFactory(
                    1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                    new List<IKeyLayout>
                    {
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWBigChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom,
                                DungeonItemID.SWWestLobbyPot
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWBigChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom,
                                        DungeonItemID.SWWestLobbyPot,
                                        DungeonItemID.SWSpikeCornerDrop
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    dungeon, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[true],
                        _itemPlacementRequirements[ItemPlacement.Basic],
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.SWBigKeyChest,
                        DungeonItemID.SWMapChest,
                        DungeonItemID.SWBigChest,
                        DungeonItemID.SWPotPrison,
                        DungeonItemID.SWCompassChest,
                        DungeonItemID.SWPinballRoom,
                        DungeonItemID.SWBridgeRoom,
                        DungeonItemID.SWBoss,
                        DungeonItemID.SWWestLobbyPot,
                        DungeonItemID.SWSpikeCornerDrop
                    },
                    new List<IKeyLayout> {_endFactory()}, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _guaranteedBossItemsRequirements[false],
                        _keyDropShuffleRequirements[true],
                        _smallKeyShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.SWBigKeyChest,
                        DungeonItemID.SWMapChest,
                        DungeonItemID.SWBigChest,
                        DungeonItemID.SWPotPrison,
                        DungeonItemID.SWCompassChest,
                        DungeonItemID.SWPinballRoom,
                        DungeonItemID.SWBridgeRoom,
                        DungeonItemID.SWWestLobbyPot,
                        DungeonItemID.SWSpikeCornerDrop
                    },
                    new List<IKeyLayout> {_endFactory()}, _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _guaranteedBossItemsRequirements[true],
                        _keyDropShuffleRequirements[true],
                        _smallKeyShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.SWBigKeyChest,
                        DungeonItemID.SWMapChest,
                        DungeonItemID.SWBigChest,
                        DungeonItemID.SWPotPrison,
                        DungeonItemID.SWCompassChest,
                        DungeonItemID.SWPinballRoom,
                        DungeonItemID.SWBridgeRoom,
                        DungeonItemID.SWWestLobbyPot
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWBigChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom,
                                DungeonItemID.SWWestLobbyPot
                            },
                            true, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWBigChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom,
                                        DungeonItemID.SWWestLobbyPot,
                                        DungeonItemID.SWSpikeCornerDrop
                                    },
                                    true, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _itemPlacementRequirements[ItemPlacement.Advanced],
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.SWBigKeyChest,
                        DungeonItemID.SWMapChest,
                        DungeonItemID.SWBigChest,
                        DungeonItemID.SWPotPrison,
                        DungeonItemID.SWCompassChest,
                        DungeonItemID.SWBridgeRoom,
                        DungeonItemID.SWWestLobbyPot
                    },
                    new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    4, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWBigChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom,
                                        DungeonItemID.SWWestLobbyPot
                                    },
                                    true, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            5, new List<DungeonItemID>
                                            {
                                                DungeonItemID.SWBigKeyChest,
                                                DungeonItemID.SWMapChest,
                                                DungeonItemID.SWBigChest,
                                                DungeonItemID.SWPotPrison,
                                                DungeonItemID.SWCompassChest,
                                                DungeonItemID.SWPinballRoom,
                                                DungeonItemID.SWBridgeRoom,
                                                DungeonItemID.SWWestLobbyPot,
                                                DungeonItemID.SWSpikeCornerDrop
                                            },
                                            true, new List<IKeyLayout> {_endFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon)
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _itemPlacementRequirements[ItemPlacement.Basic],
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.SWSpikeCornerDrop}, new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            5, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWBigChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom,
                                DungeonItemID.SWWestLobbyPot
                            },
                            false, new List<IKeyLayout> {_endFactory()}, dungeon,
                            _itemPlacementRequirements[ItemPlacement.Advanced]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWBigChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom,
                                        DungeonItemID.SWWestLobbyPot
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon, _itemPlacementRequirements[ItemPlacement.Basic])
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _keyDropShuffleRequirements[true]
                    }]),
                _bigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.SWBoss}, new List<IKeyLayout>
                    {
                        _endFactory(_smallKeyShuffleRequirements[true]),
                        _smallKeyFactory(
                            4, new List<DungeonItemID>
                            {
                                DungeonItemID.SWBigKeyChest,
                                DungeonItemID.SWMapChest,
                                DungeonItemID.SWPotPrison,
                                DungeonItemID.SWCompassChest,
                                DungeonItemID.SWPinballRoom,
                                DungeonItemID.SWBridgeRoom,
                                DungeonItemID.SWWestLobbyPot
                            },
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    5, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom,
                                        DungeonItemID.SWWestLobbyPot,
                                        DungeonItemID.SWSpikeCornerDrop
                                    },
                                    false, new List<IKeyLayout> {_endFactory()}, dungeon)
                            },
                            dungeon, _itemPlacementRequirements[ItemPlacement.Advanced]),
                        _smallKeyFactory(
                            1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                            false, new List<IKeyLayout>
                            {
                                _smallKeyFactory(
                                    4, new List<DungeonItemID>
                                    {
                                        DungeonItemID.SWBigKeyChest,
                                        DungeonItemID.SWMapChest,
                                        DungeonItemID.SWPotPrison,
                                        DungeonItemID.SWCompassChest,
                                        DungeonItemID.SWPinballRoom,
                                        DungeonItemID.SWBridgeRoom,
                                        DungeonItemID.SWWestLobbyPot
                                    },
                                    false, new List<IKeyLayout>
                                    {
                                        _smallKeyFactory(
                                            5, new List<DungeonItemID>
                                            {
                                                DungeonItemID.SWBigKeyChest,
                                                DungeonItemID.SWMapChest,
                                                DungeonItemID.SWPotPrison,
                                                DungeonItemID.SWCompassChest,
                                                DungeonItemID.SWPinballRoom,
                                                DungeonItemID.SWBridgeRoom,
                                                DungeonItemID.SWWestLobbyPot,
                                                DungeonItemID.SWSpikeCornerDrop
                                            },
                                            false, new List<IKeyLayout> {_endFactory()}, dungeon)
                                    },
                                    dungeon)
                            },
                            dungeon, _itemPlacementRequirements[ItemPlacement.Basic])
                    },
                    _aggregateRequirements[new HashSet<IRequirement>
                    {
                        _bigKeyShuffleRequirements[false],
                        _guaranteedBossItemsRequirements[false],
                        _keyDropShuffleRequirements[true]
                    }])
            }).ToExpectedObject();
            
            expected.ShouldEqual(_sut.GetDungeonKeyLayouts(dungeon));
        }
    }
}