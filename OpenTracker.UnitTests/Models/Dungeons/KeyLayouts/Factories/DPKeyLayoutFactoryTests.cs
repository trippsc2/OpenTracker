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
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories;

[ExcludeFromCodeCoverage]
public sealed class DPKeyLayoutFactoryTests
{
    private static readonly IAggregateRequirementDictionary AggregateRequirements =
        new AggregateRequirementDictionary(requirements =>
            new AggregateRequirement(requirements));
    private static readonly IBigKeyShuffleRequirementDictionary BigKeyShuffleRequirements =
        Substitute.For<IBigKeyShuffleRequirementDictionary>();
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

    private readonly DPKeyLayoutFactory _sut = new(
        AggregateRequirements, BigKeyShuffleRequirements, KeyDropShuffleRequirements,
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
                1, new List<DungeonItemID>
                {
                    DungeonItemID.DPMapChest,
                    DungeonItemID.DPTorch,
                    DungeonItemID.DPBigChest
                },
                false, new List<IKeyLayout> {EndFactory()}, dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.DPMapChest, DungeonItemID.DPTorch},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest
                        },
                        true, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.DPCompassChest, DungeonItemID.DPBigKeyChest},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1,
                        new List<DungeonItemID> {DungeonItemID.DPMapChest, DungeonItemID.DPTorch},
                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            SmallKeyFactory(
                2, new List<DungeonItemID>
                {
                    DungeonItemID.DPMapChest,
                    DungeonItemID.DPTorch,
                    DungeonItemID.DPBigChest,
                    DungeonItemID.DPTiles1Pot
                },
                false, new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest,
                            DungeonItemID.DPTiles1Pot,
                            DungeonItemID.DPBeamosHallPot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest,
                                    DungeonItemID.DPTiles1Pot,
                                    DungeonItemID.DPBeamosHallPot,
                                    DungeonItemID.DPTiles2Pot
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
                    DungeonItemID.DPMapChest,
                    DungeonItemID.DPTorch,
                    DungeonItemID.DPCompassChest,
                    DungeonItemID.DPBigKeyChest,
                    DungeonItemID.DPTiles1Pot,
                    DungeonItemID.DPBeamosHallPot,
                    DungeonItemID.DPTiles2Pot
                },
                new List<IKeyLayout> {EndFactory()}, AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true],
                    SmallKeyShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.DPMapChest,
                    DungeonItemID.DPTorch,
                    DungeonItemID.DPTiles1Pot
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPBigChest,
                            DungeonItemID.DPTiles1Pot
                        },
                        true, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest,
                                    DungeonItemID.DPTiles1Pot,
                                    DungeonItemID.DPBeamosHallPot
                                },
                                true, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        4, new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPBigChest,
                                            DungeonItemID.DPTiles1Pot,
                                            DungeonItemID.DPBeamosHallPot,
                                            DungeonItemID.DPTiles2Pot
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
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.DPBeamosHallPot},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPTiles1Pot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest,
                                    DungeonItemID.DPTiles1Pot
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        4, new List<DungeonItemID>
                                        {
                                            DungeonItemID.DPMapChest,
                                            DungeonItemID.DPTorch,
                                            DungeonItemID.DPBigChest,
                                            DungeonItemID.DPTiles1Pot,
                                            DungeonItemID.DPTiles2Pot
                                        },
                                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
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
                new List<DungeonItemID> {DungeonItemID.DPTiles2Pot},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.DPMapChest,
                            DungeonItemID.DPTorch,
                            DungeonItemID.DPTiles1Pot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.DPMapChest,
                                    DungeonItemID.DPTorch,
                                    DungeonItemID.DPBigChest,
                                    DungeonItemID.DPTiles1Pot,
                                    DungeonItemID.DPBeamosHallPot
                                },
                                false, new List<IKeyLayout> {EndFactory()}, dungeon)
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