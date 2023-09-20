using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Dungeons.Items;
using OpenTracker.Models.Dungeons.KeyLayouts;
using OpenTracker.Models.Dungeons.KeyLayouts.Factories;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.GuaranteedBossItems;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories;

[ExcludeFromCodeCoverage]
public sealed class SPKeyLayoutFactoryTests
{
    private static readonly IAggregateRequirementDictionary AggregateRequirements =
        new AggregateRequirementDictionary(requirements =>
            new AggregateRequirement(requirements));
    private static readonly IBigKeyShuffleRequirementDictionary BigKeyShuffleRequirements =
        Substitute.For<IBigKeyShuffleRequirementDictionary>();
    private static readonly IGuaranteedBossItemsRequirementDictionary GuaranteedBossItemsRequirements =
        Substitute.For<IGuaranteedBossItemsRequirementDictionary>();
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

    private readonly SPKeyLayoutFactory _sut = new(
        AggregateRequirements, BigKeyShuffleRequirements, GuaranteedBossItemsRequirements,
        KeyDropShuffleRequirements, SmallKeyShuffleRequirements,
        BigKeyFactory, EndFactory, SmallKeyFactory);

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
                1, new List<DungeonItemID> {DungeonItemID.SPEntrance}, false,
                new List<IKeyLayout> {EndFactory()}, dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.SPMapChest,
                    DungeonItemID.SPBigChest,
                    DungeonItemID.SPCompassChest,
                    DungeonItemID.SPWestChest,
                    DungeonItemID.SPBigKeyChest,
                    DungeonItemID.SPFloodedRoomLeft,
                    DungeonItemID.SPFloodedRoomRight,
                    DungeonItemID.SPWaterfallRoom
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.SPBoss}, new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    GuaranteedBossItemsRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            SmallKeyFactory(
                1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                false, new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.SPEntrance,
                            DungeonItemID.SPMapChest,
                            DungeonItemID.SPPotRowPot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot,
                                    DungeonItemID.SPTrench1Pot
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        5, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPBigChest,
                                            DungeonItemID.SPCompassChest,
                                            DungeonItemID.SPTrench1Pot,
                                            DungeonItemID.SPHookshotPot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            SmallKeyFactory(
                                                6, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPBigChest,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPFloodedRoomLeft,
                                                    DungeonItemID.SPFloodedRoomRight,
                                                    DungeonItemID.SPWaterfallRoom,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot,
                                                    DungeonItemID.SPWaterwayPot
                                                },
                                                false, new List<IKeyLayout> {EndFactory()},
                                                dungeon)
                                        }, dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
                },
                dungeon, AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.SPMapChest, DungeonItemID.SPPotRowPot},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                true, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        true, new List<IKeyLayout>
                                        {
                                            SmallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPBigChest,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                true, new List<IKeyLayout>
                                                {
                                                    SmallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPBigChest,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        true, new List<IKeyLayout>
                                                        {
                                                            EndFactory()
                                                        }, dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.SPTrench1Pot},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            SmallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPBigChest,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                false, new List<IKeyLayout>
                                                {
                                                    SmallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPBigChest,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        false, new List<IKeyLayout>
                                                        {
                                                            EndFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.SPBigChest,
                    DungeonItemID.SPCompassChest,
                    DungeonItemID.SPHookshotPot
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            SmallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPBigChest,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                true, new List<IKeyLayout>
                                                {
                                                    SmallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPBigChest,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        true, new List<IKeyLayout>
                                                        {
                                                            EndFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.SPFloodedRoomLeft,
                    DungeonItemID.SPFloodedRoomRight,
                    DungeonItemID.SPWaterfallRoom,
                    DungeonItemID.SPWaterwayPot
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            SmallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                false, new List<IKeyLayout>
                                                {
                                                    SmallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPBigChest,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        true, new List<IKeyLayout>
                                                        {
                                                            EndFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.SPWestChest,
                    DungeonItemID.SPBigKeyChest,
                    DungeonItemID.SPTrench2Pot
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            SmallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                false, new List<IKeyLayout>
                                                {
                                                    SmallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        false, new List<IKeyLayout>
                                                        {
                                                            EndFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.SPBoss}, new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID> {DungeonItemID.SPEntrance},
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.SPEntrance,
                                    DungeonItemID.SPMapChest,
                                    DungeonItemID.SPPotRowPot
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.SPEntrance,
                                            DungeonItemID.SPMapChest,
                                            DungeonItemID.SPPotRowPot,
                                            DungeonItemID.SPTrench1Pot
                                        },
                                        false, new List<IKeyLayout>
                                        {
                                            SmallKeyFactory(
                                                5, new List<DungeonItemID>
                                                {
                                                    DungeonItemID.SPEntrance,
                                                    DungeonItemID.SPMapChest,
                                                    DungeonItemID.SPPotRowPot,
                                                    DungeonItemID.SPCompassChest,
                                                    DungeonItemID.SPTrench1Pot,
                                                    DungeonItemID.SPHookshotPot
                                                },
                                                false, new List<IKeyLayout>
                                                {
                                                    SmallKeyFactory(
                                                        6, new List<DungeonItemID>
                                                        {
                                                            DungeonItemID.SPEntrance,
                                                            DungeonItemID.SPMapChest,
                                                            DungeonItemID.SPPotRowPot,
                                                            DungeonItemID.SPCompassChest,
                                                            DungeonItemID.SPFloodedRoomLeft,
                                                            DungeonItemID.SPFloodedRoomRight,
                                                            DungeonItemID.SPWaterfallRoom,
                                                            DungeonItemID.SPTrench1Pot,
                                                            DungeonItemID.SPHookshotPot,
                                                            DungeonItemID.SPWaterwayPot
                                                        },
                                                        false, new List<IKeyLayout>
                                                        {
                                                            EndFactory()
                                                        },
                                                        dungeon)
                                                },
                                                dungeon)
                                        },
                                        dungeon)
                                },
                                dungeon)
                        },
                        dungeon)
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