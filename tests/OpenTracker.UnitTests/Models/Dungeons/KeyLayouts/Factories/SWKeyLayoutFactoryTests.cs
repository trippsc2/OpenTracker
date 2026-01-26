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

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories;

public class SWKeyLayoutFactoryTests
{
    private static readonly IAggregateRequirementDictionary AggregateRequirements =
        new AggregateRequirementDictionary(requirements =>
            new AggregateRequirement(requirements));
    private static readonly IBigKeyShuffleRequirementDictionary BigKeyShuffleRequirements =
        Substitute.For<IBigKeyShuffleRequirementDictionary>();
    private static readonly IGuaranteedBossItemsRequirementDictionary GuaranteedBossItemsRequirements =
        Substitute.For<IGuaranteedBossItemsRequirementDictionary>();
    private static readonly IItemPlacementRequirementDictionary ItemPlacementRequirements =
        Substitute.For<IItemPlacementRequirementDictionary>();
    private static readonly IKeyDropShuffleRequirementDictionary KeyDropShuffleRequirements =
        Substitute.For<IKeyDropShuffleRequirementDictionary>();
    private static readonly ISmallKeyShuffleRequirementDictionary SmallKeyShuffleRequirements =
        Substitute.For<ISmallKeyShuffleRequirementDictionary>();

    private static readonly IBigKeyLayout.Factory BigKeyFactory = (bigKeyLocations, children, requirement) =>
        new BigKeyLayout(bigKeyLocations, children, requirement);
    private static readonly IEndKeyLayout.Factory EndFactory = requirement => new EndKeyLayout(requirement);
    private static readonly ISmallKeyLayout.Factory SmallKeyFactory =
        (count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement) => new SmallKeyLayout(
            count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement);

    private readonly SWKeyLayoutFactory _sut = new(
        AggregateRequirements, BigKeyShuffleRequirements, GuaranteedBossItemsRequirements,
        ItemPlacementRequirements, KeyDropShuffleRequirements,
        SmallKeyShuffleRequirements, BigKeyFactory, EndFactory,
        SmallKeyFactory);

    [Fact]
    public void GetDungeonKeyLayouts_ShouldReturnExpected()
    {
        var dungeon = Substitute.For<IDungeon>();
            
        var expected = (new List<IKeyLayout>
        {
            EndFactory(AggregateRequirements[new HashSet<IRequirement>
            {
                BigKeyShuffleRequirements[true],
                SmallKeyShuffleRequirements[true]
            }]),
            SmallKeyFactory(
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
                false, new List<IKeyLayout> {EndFactory()}, dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    ItemPlacementRequirements[ItemPlacement.Advanced],
                    KeyDropShuffleRequirements[false],
                }]),
            SmallKeyFactory(
                1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                new List<IKeyLayout>
                {
                    SmallKeyFactory(
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
                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                dungeon, AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    ItemPlacementRequirements[ItemPlacement.Basic],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
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
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
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
                        true, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    ItemPlacementRequirements[ItemPlacement.Advanced],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
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
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
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
                                true, new List<IKeyLayout> {EndFactory()}, dungeon)
                        }, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    ItemPlacementRequirements[ItemPlacement.Basic],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.SWBoss}, new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.SWBigKeyChest,
                            DungeonItemID.SWMapChest,
                            DungeonItemID.SWPotPrison,
                            DungeonItemID.SWCompassChest,
                            DungeonItemID.SWPinballRoom,
                            DungeonItemID.SWBridgeRoom
                        },
                        false, new List<IKeyLayout> {EndFactory()}, dungeon,
                        ItemPlacementRequirements[ItemPlacement.Advanced]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.SWBigKeyChest,
                                    DungeonItemID.SWMapChest,
                                    DungeonItemID.SWPotPrison,
                                    DungeonItemID.SWCompassChest,
                                    DungeonItemID.SWPinballRoom,
                                    DungeonItemID.SWBridgeRoom
                                },
                                false, new List<IKeyLayout> {EndFactory()}, dungeon)
                        },
                        dungeon, ItemPlacementRequirements[ItemPlacement.Basic])
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    GuaranteedBossItemsRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            SmallKeyFactory(
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
                    SmallKeyFactory(
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
                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    ItemPlacementRequirements[ItemPlacement.Advanced],
                    KeyDropShuffleRequirements[true]
                }]),
            SmallKeyFactory(
                1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom}, false,
                new List<IKeyLayout>
                {
                    SmallKeyFactory(
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
                            SmallKeyFactory(
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
                                false, new List<IKeyLayout> {EndFactory()}, dungeon)
                        },
                        dungeon)
                },
                dungeon, AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    ItemPlacementRequirements[ItemPlacement.Basic],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
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
                new List<IKeyLayout> {EndFactory()}, AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    GuaranteedBossItemsRequirements[false],
                    KeyDropShuffleRequirements[true],
                    SmallKeyShuffleRequirements[true]
                }]),
            BigKeyFactory(
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
                new List<IKeyLayout> {EndFactory()}, AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    GuaranteedBossItemsRequirements[true],
                    KeyDropShuffleRequirements[true],
                    SmallKeyShuffleRequirements[true]
                }]),
            BigKeyFactory(
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
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
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
                            SmallKeyFactory(
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
                                true, new List<IKeyLayout> {EndFactory()}, dungeon)
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    ItemPlacementRequirements[ItemPlacement.Advanced],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
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
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
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
                                    SmallKeyFactory(
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
                                        true, new List<IKeyLayout> {EndFactory()}, dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    ItemPlacementRequirements[ItemPlacement.Basic],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.SWSpikeCornerDrop}, new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
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
                        false, new List<IKeyLayout> {EndFactory()}, dungeon,
                        ItemPlacementRequirements[ItemPlacement.Advanced]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
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
                                false, new List<IKeyLayout> {EndFactory()}, dungeon)
                        },
                        dungeon, ItemPlacementRequirements[ItemPlacement.Basic])
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.SWBoss}, new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
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
                            SmallKeyFactory(
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
                                false, new List<IKeyLayout> {EndFactory()}, dungeon)
                        },
                        dungeon, ItemPlacementRequirements[ItemPlacement.Advanced]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SWPinballRoom},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
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
                                    SmallKeyFactory(
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
                                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
                                },
                                dungeon)
                        },
                        dungeon, ItemPlacementRequirements[ItemPlacement.Basic])
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    GuaranteedBossItemsRequirements[false],
                    KeyDropShuffleRequirements[true]
                }])
        }).ToExpectedObject();
            
        expected.ShouldEqual(_sut.GetDungeonKeyLayouts(dungeon));
    }
}