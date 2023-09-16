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

public class GTKeyLayoutFactoryTests
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

    private readonly GTKeyLayoutFactory _sut = new(
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
                3, new List<DungeonItemID>
                {
                    DungeonItemID.GTHopeRoomLeft,
                    DungeonItemID.GTHopeRoomRight,
                    DungeonItemID.GTBobsTorch,
                    DungeonItemID.GTDMsRoomTopLeft,
                    DungeonItemID.GTDMsRoomTopRight,
                    DungeonItemID.GTDMsRoomBottomLeft,
                    DungeonItemID.GTDMsRoomBottomRight,
                    DungeonItemID.GTTileRoom
                },
                false, new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        4, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTMapChest,
                            DungeonItemID.GTFiresnakeRoom,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTBobsChest,
                            DungeonItemID.GTBigKeyRoomTopLeft,
                            DungeonItemID.GTBigKeyRoomTopRight,
                            DungeonItemID.GTBigKeyChest,
                            DungeonItemID.GTBigChest,
                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                            DungeonItemID.GTMiniHelmasaurRoomRight,
                            DungeonItemID.GTPreMoldormChest
                        },
                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                dungeon, AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.GTHopeRoomLeft,
                    DungeonItemID.GTHopeRoomRight,
                    DungeonItemID.GTBobsTorch,
                    DungeonItemID.GTDMsRoomTopLeft,
                    DungeonItemID.GTDMsRoomTopRight,
                    DungeonItemID.GTDMsRoomBottomLeft,
                    DungeonItemID.GTDMsRoomBottomRight,
                    DungeonItemID.GTTileRoom
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        true, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTMapChest,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest
                                },
                                true, new List<IKeyLayout> {EndFactory()}, dungeon)
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
                    DungeonItemID.GTFiresnakeRoom,
                    DungeonItemID.GTBobsChest,
                    DungeonItemID.GTBigKeyRoomTopLeft,
                    DungeonItemID.GTBigKeyRoomTopRight,
                    DungeonItemID.GTBigKeyChest
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTMapChest,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest
                                },
                                true, new List<IKeyLayout> {EndFactory()}, dungeon)
                        },
                        dungeon, SmallKeyShuffleRequirements[false])
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.GTMapChest}, new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest
                                },
                                false, new List<IKeyLayout> {EndFactory()}, dungeon)
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
                    DungeonItemID.GTRandomizerRoomTopLeft,
                    DungeonItemID.GTRandomizerRoomTopRight,
                    DungeonItemID.GTRandomizerRoomBottomLeft,
                    DungeonItemID.GTRandomizerRoomBottomRight
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        4, new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTMapChest,
                                            DungeonItemID.GTFiresnakeRoom,
                                            DungeonItemID.GTRandomizerRoomTopLeft,
                                            DungeonItemID.GTRandomizerRoomTopRight,
                                            DungeonItemID.GTRandomizerRoomBottomLeft,
                                            DungeonItemID.GTRandomizerRoomBottomRight,
                                            DungeonItemID.GTTileRoom,
                                            DungeonItemID.GTBobsChest,
                                            DungeonItemID.GTBigKeyRoomTopLeft,
                                            DungeonItemID.GTBigKeyRoomTopRight,
                                            DungeonItemID.GTBigKeyChest
                                        },
                                        true, new List<IKeyLayout> {EndFactory()},
                                        dungeon)
                                },
                                dungeon)
                        }, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.GTCompassRoomTopLeft,
                    DungeonItemID.GTCompassRoomTopRight,
                    DungeonItemID.GTCompassRoomBottomLeft,
                    DungeonItemID.GTCompassRoomBottomRight
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        2, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                3, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom
                                },
                                false, new List<IKeyLayout>
                                {
                                    SmallKeyFactory(
                                        4, new List<DungeonItemID>
                                        {
                                            DungeonItemID.GTHopeRoomLeft,
                                            DungeonItemID.GTHopeRoomRight,
                                            DungeonItemID.GTBobsTorch,
                                            DungeonItemID.GTDMsRoomTopLeft,
                                            DungeonItemID.GTDMsRoomTopRight,
                                            DungeonItemID.GTDMsRoomBottomLeft,
                                            DungeonItemID.GTDMsRoomBottomRight,
                                            DungeonItemID.GTMapChest,
                                            DungeonItemID.GTFiresnakeRoom,
                                            DungeonItemID.GTTileRoom,
                                            DungeonItemID.GTCompassRoomTopLeft,
                                            DungeonItemID.GTCompassRoomTopRight,
                                            DungeonItemID.GTCompassRoomBottomLeft,
                                            DungeonItemID.GTCompassRoomBottomRight,
                                            DungeonItemID.GTBobsChest,
                                            DungeonItemID.GTBigKeyRoomTopLeft,
                                            DungeonItemID.GTBigKeyRoomTopRight,
                                            DungeonItemID.GTBigKeyChest
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
                    KeyDropShuffleRequirements[false]
                }]),
            SmallKeyFactory(
                7, new List<DungeonItemID>
                {
                    DungeonItemID.GTHopeRoomLeft,
                    DungeonItemID.GTHopeRoomRight,
                    DungeonItemID.GTBobsTorch,
                    DungeonItemID.GTDMsRoomTopLeft,
                    DungeonItemID.GTDMsRoomTopRight,
                    DungeonItemID.GTDMsRoomBottomLeft,
                    DungeonItemID.GTDMsRoomBottomRight,
                    DungeonItemID.GTTileRoom,
                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                    DungeonItemID.GTMiniHelmasaurRoomRight,
                    DungeonItemID.GTConveyorCrossPot,
                    DungeonItemID.GTDoubleSwitchPot,
                    DungeonItemID.GTMiniHelmasaurDrop
                },
                false, new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        8, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTFiresnakeRoom,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTCompassRoomTopLeft,
                            DungeonItemID.GTCompassRoomTopRight,
                            DungeonItemID.GTCompassRoomBottomLeft,
                            DungeonItemID.GTCompassRoomBottomRight,
                            DungeonItemID.GTBobsChest,
                            DungeonItemID.GTBigKeyRoomTopLeft,
                            DungeonItemID.GTBigKeyRoomTopRight,
                            DungeonItemID.GTBigKeyChest,
                            DungeonItemID.GTBigChest,
                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                            DungeonItemID.GTMiniHelmasaurRoomRight,
                            DungeonItemID.GTPreMoldormChest,
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot,
                            DungeonItemID.GTConveyorStarPitsPot,
                            DungeonItemID.GTMiniHelmasaurDrop
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
                    DungeonItemID.GTHopeRoomLeft,
                    DungeonItemID.GTHopeRoomRight,
                    DungeonItemID.GTBobsTorch,
                    DungeonItemID.GTDMsRoomTopLeft,
                    DungeonItemID.GTDMsRoomTopRight,
                    DungeonItemID.GTDMsRoomBottomLeft,
                    DungeonItemID.GTDMsRoomBottomRight,
                    DungeonItemID.GTTileRoom,
                    DungeonItemID.GTConveyorCrossPot,
                    DungeonItemID.GTDoubleSwitchPot
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        7, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTMiniHelmasaurRoomLeft,
                            DungeonItemID.GTMiniHelmasaurRoomRight,
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot,
                            DungeonItemID.GTMiniHelmasaurDrop
                        },
                        true, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                8, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest,
                                    DungeonItemID.GTConveyorCrossPot,
                                    DungeonItemID.GTDoubleSwitchPot,
                                    DungeonItemID.GTConveyorStarPitsPot,
                                    DungeonItemID.GTMiniHelmasaurDrop
                                },
                                true, new List<IKeyLayout> {EndFactory()}, dungeon)
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
                    DungeonItemID.GTFiresnakeRoom,
                    DungeonItemID.GTCompassRoomTopLeft,
                    DungeonItemID.GTCompassRoomTopRight,
                    DungeonItemID.GTCompassRoomBottomLeft,
                    DungeonItemID.GTCompassRoomBottomRight,
                    DungeonItemID.GTBobsChest,
                    DungeonItemID.GTBigKeyRoomTopLeft,
                    DungeonItemID.GTBigKeyRoomTopRight,
                    DungeonItemID.GTBigKeyChest,
                    DungeonItemID.GTConveyorStarPitsPot
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        7, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                8, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTBigChest,
                                    DungeonItemID.GTMiniHelmasaurRoomLeft,
                                    DungeonItemID.GTMiniHelmasaurRoomRight,
                                    DungeonItemID.GTPreMoldormChest,
                                    DungeonItemID.GTConveyorCrossPot,
                                    DungeonItemID.GTDoubleSwitchPot,
                                    DungeonItemID.GTConveyorStarPitsPot,
                                    DungeonItemID.GTMiniHelmasaurDrop
                                },
                                true, new List<IKeyLayout> {EndFactory()}, dungeon)
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
                    DungeonItemID.GTRandomizerRoomTopLeft,
                    DungeonItemID.GTRandomizerRoomTopRight,
                    DungeonItemID.GTRandomizerRoomBottomLeft,
                    DungeonItemID.GTRandomizerRoomBottomRight
                },
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        7, new List<DungeonItemID>
                        {
                            DungeonItemID.GTHopeRoomLeft,
                            DungeonItemID.GTHopeRoomRight,
                            DungeonItemID.GTBobsTorch,
                            DungeonItemID.GTDMsRoomTopLeft,
                            DungeonItemID.GTDMsRoomTopRight,
                            DungeonItemID.GTDMsRoomBottomLeft,
                            DungeonItemID.GTDMsRoomBottomRight,
                            DungeonItemID.GTTileRoom,
                            DungeonItemID.GTConveyorCrossPot,
                            DungeonItemID.GTDoubleSwitchPot
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                8, new List<DungeonItemID>
                                {
                                    DungeonItemID.GTHopeRoomLeft,
                                    DungeonItemID.GTHopeRoomRight,
                                    DungeonItemID.GTBobsTorch,
                                    DungeonItemID.GTDMsRoomTopLeft,
                                    DungeonItemID.GTDMsRoomTopRight,
                                    DungeonItemID.GTDMsRoomBottomLeft,
                                    DungeonItemID.GTDMsRoomBottomRight,
                                    DungeonItemID.GTFiresnakeRoom,
                                    DungeonItemID.GTTileRoom,
                                    DungeonItemID.GTCompassRoomTopLeft,
                                    DungeonItemID.GTCompassRoomTopRight,
                                    DungeonItemID.GTCompassRoomBottomLeft,
                                    DungeonItemID.GTCompassRoomBottomRight,
                                    DungeonItemID.GTBobsChest,
                                    DungeonItemID.GTBigKeyRoomTopLeft,
                                    DungeonItemID.GTBigKeyRoomTopRight,
                                    DungeonItemID.GTBigKeyChest,
                                    DungeonItemID.GTConveyorCrossPot,
                                    DungeonItemID.GTDoubleSwitchPot,
                                    DungeonItemID.GTConveyorStarPitsPot
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