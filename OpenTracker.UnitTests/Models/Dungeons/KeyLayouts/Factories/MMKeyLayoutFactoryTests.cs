using System.Collections.Generic;
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

public class MMKeyLayoutFactoryTests
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

    private readonly MMKeyLayoutFactory _sut = new(
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
                3, new List<DungeonItemID>
                {
                    DungeonItemID.MMBridgeChest,
                    DungeonItemID.MMSpikeChest,
                    DungeonItemID.MMMainLobby,
                    DungeonItemID.MMBigChest,
                    DungeonItemID.MMMapChest,
                    DungeonItemID.MMBoss
                },
                false, new List<IKeyLayout> {EndFactory()}, dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    GuaranteedBossItemsRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            SmallKeyFactory(
                3, new List<DungeonItemID>
                {
                    DungeonItemID.MMBridgeChest,
                    DungeonItemID.MMSpikeChest,
                    DungeonItemID.MMMainLobby,
                    DungeonItemID.MMBigChest,
                    DungeonItemID.MMMapChest
                },
                true, new List<IKeyLayout> {EndFactory()}, dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    GuaranteedBossItemsRequirements[true],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.MMBridgeChest, DungeonItemID.MMSpikeChest},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMMapChest,
                            DungeonItemID.MMBoss
                        },
                        true, new List<IKeyLayout> {EndFactory()}, dungeon,
                        GuaranteedBossItemsRequirements[false]),
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMMapChest
                        },
                        true, new List<IKeyLayout> {EndFactory()}, dungeon,
                        GuaranteedBossItemsRequirements[true])
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.MMMainLobby, DungeonItemID.MMMapChest},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMBoss
                                },
                                true, new List<IKeyLayout> {EndFactory()}, dungeon,
                                GuaranteedBossItemsRequirements[false]),
                            SmallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest
                                },
                                true, new List<IKeyLayout> {EndFactory()}, dungeon,
                                GuaranteedBossItemsRequirements[true])
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.MMCompassChest,
                    DungeonItemID.MMBigKeyChest
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                2, new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMMapChest
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMCompassChest,
                                            DungeonItemID.MMBigKeyChest,
                                            DungeonItemID.MMMapChest,
                                            DungeonItemID.MMBoss
                                        },
                                        true, new List<IKeyLayout> {EndFactory()},
                                        dungeon, GuaranteedBossItemsRequirements[false]),
                                    SmallKeyFactory(
                                        3, new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMCompassChest,
                                            DungeonItemID.MMBigKeyChest,
                                            DungeonItemID.MMMapChest
                                        },
                                        true, new List<IKeyLayout> {EndFactory()},
                                        dungeon, GuaranteedBossItemsRequirements[true])
                                },
                                dungeon)
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            SmallKeyFactory(
                1, new List<DungeonItemID>
                {
                    DungeonItemID.MMBridgeChest,
                    DungeonItemID.MMSpikeChest,
                    DungeonItemID.MMBigChest,
                    DungeonItemID.MMSpikesPot,
                    DungeonItemID.MMFishbonePot
                },
                false, new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        5, new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMMainLobby,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMMapChest,
                            DungeonItemID.MMSpikesPot,
                            DungeonItemID.MMFishbonePot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                6, new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMSpikesPot,
                                    DungeonItemID.MMFishbonePot,
                                    DungeonItemID.MMConveyerCrystalDrop
                                },
                                false, new List<IKeyLayout> {EndFactory()}, dungeon)
                        },
                        dungeon)
                },
                dungeon, AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.MMBridgeChest,
                    DungeonItemID.MMSpikeChest,
                    DungeonItemID.MMSpikesPot
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMBigChest,
                            DungeonItemID.MMSpikesPot,
                            DungeonItemID.MMFishbonePot
                        },
                        true, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                5, new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMSpikesPot,
                                    DungeonItemID.MMFishbonePot
                                },
                                true, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        6, new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest,
                                            DungeonItemID.MMSpikesPot,
                                            DungeonItemID.MMFishbonePot,
                                            DungeonItemID.MMConveyerCrystalDrop
                                        },
                                        true, new List<IKeyLayout> {EndFactory()},
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
                    DungeonItemID.MMMainLobby,
                    DungeonItemID.MMMapChest,
                    DungeonItemID.MMFishbonePot
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMSpikesPot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                5, new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMBigChest,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMSpikesPot,
                                    DungeonItemID.MMFishbonePot
                                },
                                true, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        6, new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest,
                                            DungeonItemID.MMSpikesPot,
                                            DungeonItemID.MMFishbonePot,
                                            DungeonItemID.MMConveyerCrystalDrop
                                        },
                                        true, new List<IKeyLayout> {EndFactory()},
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
                new List<DungeonItemID> {DungeonItemID.MMConveyerCrystalDrop},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMSpikesPot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                5, new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMSpikesPot,
                                    DungeonItemID.MMFishbonePot
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        6, new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMBigChest,
                                            DungeonItemID.MMMapChest,
                                            DungeonItemID.MMSpikesPot,
                                            DungeonItemID.MMFishbonePot,
                                            DungeonItemID.MMConveyerCrystalDrop
                                        },
                                        true, new List<IKeyLayout> {EndFactory()},
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
                    DungeonItemID.MMCompassChest,
                    DungeonItemID.MMBigKeyChest
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.MMBridgeChest,
                            DungeonItemID.MMSpikeChest,
                            DungeonItemID.MMSpikesPot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                5, new List<DungeonItemID>
                                {
                                    DungeonItemID.MMBridgeChest,
                                    DungeonItemID.MMSpikeChest,
                                    DungeonItemID.MMMainLobby,
                                    DungeonItemID.MMMapChest,
                                    DungeonItemID.MMSpikesPot,
                                    DungeonItemID.MMFishbonePot
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        6, new List<DungeonItemID>
                                        {
                                            DungeonItemID.MMBridgeChest,
                                            DungeonItemID.MMSpikeChest,
                                            DungeonItemID.MMMainLobby,
                                            DungeonItemID.MMMapChest,
                                            DungeonItemID.MMSpikesPot,
                                            DungeonItemID.MMFishbonePot,
                                            DungeonItemID.MMConveyerCrystalDrop
                                        },
                                        false, new List<IKeyLayout> {EndFactory()},
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
                }])
        }).ToExpectedObject();
            
        expected.ShouldEqual(_sut.GetDungeonKeyLayouts(dungeon));
    }
}