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
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories;

public class TTKeyLayoutFactoryTests
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

    private readonly TTKeyLayoutFactory _sut = new(
        AggregateRequirements, BigKeyShuffleRequirements, KeyDropShuffleRequirements, SmallKeyShuffleRequirements,
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
                1, new List<DungeonItemID>
                {
                    DungeonItemID.TTMapChest,
                    DungeonItemID.TTAmbushChest,
                    DungeonItemID.TTCompassChest,
                    DungeonItemID.TTBigKeyChest,
                    DungeonItemID.TTBlindsCell,
                    DungeonItemID.TTBigChest
                },
                false, new List<IKeyLayout> {EndFactory()}, dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.TTMapChest,
                    DungeonItemID.TTAmbushChest,
                    DungeonItemID.TTCompassChest,
                    DungeonItemID.TTBigKeyChest
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.TTMapChest,
                            DungeonItemID.TTAmbushChest,
                            DungeonItemID.TTCompassChest,
                            DungeonItemID.TTBigKeyChest,
                            DungeonItemID.TTBlindsCell,
                            DungeonItemID.TTBigChest
                        },
                        true, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            SmallKeyFactory(
                1, new List<DungeonItemID>
                {
                    DungeonItemID.TTMapChest,
                    DungeonItemID.TTAmbushChest,
                    DungeonItemID.TTCompassChest,
                    DungeonItemID.TTBigKeyChest,
                    DungeonItemID.TTHallwayPot
                },
                false, new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.TTMapChest,
                            DungeonItemID.TTAmbushChest,
                            DungeonItemID.TTCompassChest,
                            DungeonItemID.TTBigKeyChest,
                            DungeonItemID.TTBlindsCell,
                            DungeonItemID.TTHallwayPot,
                            DungeonItemID.TTSpikeSwitchPot
                        },
                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                dungeon, AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    KeyDropShuffleRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.TTMapChest,
                    DungeonItemID.TTAmbushChest,
                    DungeonItemID.TTCompassChest,
                    DungeonItemID.TTBigKeyChest
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.TTMapChest,
                            DungeonItemID.TTAmbushChest,
                            DungeonItemID.TTCompassChest,
                            DungeonItemID.TTBigKeyChest,
                            DungeonItemID.TTHallwayPot
                        },
                        true, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.TTMapChest,
                                    DungeonItemID.TTAmbushChest,
                                    DungeonItemID.TTCompassChest,
                                    DungeonItemID.TTBigKeyChest,
                                    DungeonItemID.TTBlindsCell,
                                    DungeonItemID.TTHallwayPot,
                                    DungeonItemID.TTSpikeSwitchPot
                                },
                                true, new List<IKeyLayout> {EndFactory()}, dungeon)
                        }, dungeon)
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