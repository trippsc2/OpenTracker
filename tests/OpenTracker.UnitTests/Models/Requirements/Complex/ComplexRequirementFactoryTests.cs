using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.Dungeons;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Prizes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.EnemyShuffle;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Exact;
using OpenTracker.Models.Requirements.Item.Prize;
using OpenTracker.Models.Requirements.Item.SmallKey;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using OpenTracker.Models.Requirements.Static;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Complex;

[ExcludeFromCodeCoverage]
public sealed class ComplexRequirementFactoryTests
{
    private static readonly IAggregateRequirementDictionary AggregateRequirements =
        new AggregateRequirementDictionary(
            requirements => new AggregateRequirement(requirements));
    private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
        new AlternativeRequirementDictionary(
            requirements => new AlternativeRequirement(requirements));
    private static readonly IComplexRequirementDictionary ComplexRequirements =
        Substitute.For<IComplexRequirementDictionary>();
    private static readonly IEnemyShuffleRequirementDictionary EnemyShuffleRequirements =
        Substitute.For<IEnemyShuffleRequirementDictionary>();
    private static readonly IEntranceShuffleRequirementDictionary EntranceShuffleRequirements =
        Substitute.For<IEntranceShuffleRequirementDictionary>();
    private static readonly IItemRequirementDictionary ItemRequirements =
        Substitute.For<IItemRequirementDictionary>();
    private static readonly IItemExactRequirementDictionary ItemExactRequirements =
        Substitute.For<IItemExactRequirementDictionary>();
    private static readonly IItemPlacementRequirementDictionary ItemPlacementRequirements =
        Substitute.For<IItemPlacementRequirementDictionary>();
    private static readonly IKeyDropShuffleRequirementDictionary KeyDropShuffleRequirements =
        Substitute.For<IKeyDropShuffleRequirementDictionary>();
    private static readonly IPrizeRequirementDictionary PrizeRequirements =
        Substitute.For<IPrizeRequirementDictionary>();
    private static readonly ISequenceBreakRequirementDictionary SequenceBreakRequirements =
        Substitute.For<ISequenceBreakRequirementDictionary>();
    private static readonly ISmallKeyRequirementDictionary SmallKeyRequirements =
        Substitute.For<ISmallKeyRequirementDictionary>();
    private static readonly ISmallKeyShuffleRequirementDictionary SmallKeyShuffleRequirements =
        Substitute.For<ISmallKeyShuffleRequirementDictionary>();
    private static readonly IStaticRequirementDictionary StaticRequirements =
        Substitute.For<IStaticRequirementDictionary>();
    private static readonly IWorldStateRequirementDictionary WorldStateRequirements =
        Substitute.For<IWorldStateRequirementDictionary>();

    private static readonly Dictionary<ComplexRequirementType, ExpectedObject> ExpectedValues = new();

    private readonly ComplexRequirementFactory _sut;

    public ComplexRequirementFactoryTests()
    {
        _sut = new ComplexRequirementFactory(
            AggregateRequirements, AlternativeRequirements, ComplexRequirements, EnemyShuffleRequirements,
            EntranceShuffleRequirements, ItemRequirements, ItemExactRequirements, ItemPlacementRequirements,
            KeyDropShuffleRequirements, PrizeRequirements, SequenceBreakRequirements, SmallKeyRequirements,
            SmallKeyShuffleRequirements, StaticRequirements, WorldStateRequirements);
    }

    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (ComplexRequirementType type in Enum.GetValues(typeof(ComplexRequirementType)))
        {
            ExpectedValues.Add(type, (type switch
            {
                ComplexRequirementType.AllMedallions => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Bombos, 1)],
                    ItemRequirements[(ItemType.Ether, 1)],
                    ItemRequirements[(ItemType.Quake, 1)]
                }],
                ComplexRequirementType.ExtendMagic1 => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Bottle, 1)],
                    ItemRequirements[(ItemType.HalfMagic, 1)]
                }],
                ComplexRequirementType.ExtendMagic2 => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Bottle, 1)],
                    ItemRequirements[(ItemType.HalfMagic, 1)]
                }],
                ComplexRequirementType.FireRodDarkRoom => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.FireRod, 1)],
                    ItemPlacementRequirements[ItemPlacement.Advanced]
                }],
                ComplexRequirementType.UseMedallion => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemExactRequirements[(ItemType.Sword, 0)],
                    ItemRequirements[(ItemType.Sword, 2)]
                }],
                ComplexRequirementType.MeltThings => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.FireRod, 1)],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Bombos, 1)],
                        ComplexRequirements[ComplexRequirementType.UseMedallion]
                    }]
                }],
                ComplexRequirementType.NotBunnyLW => AlternativeRequirements[new HashSet<IRequirement>
                {
                    WorldStateRequirements[WorldState.StandardOpen],
                    ItemRequirements[(ItemType.MoonPearl, 1)]
                }],
                ComplexRequirementType.NotBunnyDW => AlternativeRequirements[new HashSet<IRequirement>
                {
                    WorldStateRequirements[WorldState.Inverted],
                    ItemRequirements[(ItemType.MoonPearl, 1)]
                }],
                ComplexRequirementType.ATBarrier => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Cape, 1)],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        ItemExactRequirements[(ItemType.Sword, 0)],
                        ItemRequirements[(ItemType.Hammer, 1)]
                    }],
                    ItemRequirements[(ItemType.Sword, 3)]
                }],
                ComplexRequirementType.BombDuplicationAncillaOverload =>
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        SequenceBreakRequirements[SequenceBreakType.BombDuplicationAncillaOverload],
                        ItemRequirements[(ItemType.Bow, 1)],
                        ItemRequirements[(ItemType.CaneOfSomaria, 1)],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Boomerang, 1)],
                            ItemRequirements[(ItemType.RedBoomerang, 1)]
                        }]
                    }],
                ComplexRequirementType.BombDuplicationMirror => AggregateRequirements[new HashSet<IRequirement>
                {
                    SequenceBreakRequirements[SequenceBreakType.BombDuplicationMirror],
                    ItemRequirements[(ItemType.Flippers, 1)],
                    ItemRequirements[(ItemType.Mirror, 1)]
                }],
                ComplexRequirementType.BonkOverLedge => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Boots, 1)],
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemPlacementRequirements[ItemPlacement.Advanced],
                        SequenceBreakRequirements[SequenceBreakType.BonkOverLedge]
                    }]
                }],
                ComplexRequirementType.BumperCaveGap => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Hookshot, 1)],
                    ItemPlacementRequirements[ItemPlacement.Advanced],
                    SequenceBreakRequirements[SequenceBreakType.BumperCaveHookshot]
                }],
                ComplexRequirementType.CameraUnlock => AggregateRequirements[new HashSet<IRequirement>
                {
                    SequenceBreakRequirements[SequenceBreakType.CameraUnlock],
                    ItemRequirements[(ItemType.Bottle, 1)]
                }],
                ComplexRequirementType.Curtains => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemExactRequirements[(ItemType.Sword, 0)],
                    ItemRequirements[(ItemType.Sword, 2)]
                }],
                ComplexRequirementType.DarkRoomDeathMountainEntry => AlternativeRequirements[
                    new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Lamp, 1)],
                        SequenceBreakRequirements[SequenceBreakType.DarkRoomDeathMountainEntry]
                    }],
                ComplexRequirementType.DarkRoomDeathMountainExit => AlternativeRequirements[
                    new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Lamp, 1)],
                        SequenceBreakRequirements[SequenceBreakType.DarkRoomDeathMountainExit]
                    }],
                ComplexRequirementType.DarkRoomHC => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    ComplexRequirements[ComplexRequirementType.FireRodDarkRoom],
                    SequenceBreakRequirements[SequenceBreakType.DarkRoomHC]
                }],
                ComplexRequirementType.DarkRoomAT => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    SequenceBreakRequirements[SequenceBreakType.DarkRoomAT]
                }],
                ComplexRequirementType.DarkRoomEPRight => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    SequenceBreakRequirements[SequenceBreakType.DarkRoomEPRight]
                }],
                ComplexRequirementType.DarkRoomEPBack => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    ComplexRequirements[ComplexRequirementType.FireRodDarkRoom],
                    SequenceBreakRequirements[SequenceBreakType.DarkRoomEPBack]
                }],
                ComplexRequirementType.DarkRoomPoDDarkBasement => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    ComplexRequirements[ComplexRequirementType.FireRodDarkRoom],
                    SequenceBreakRequirements[SequenceBreakType.DarkRoomPoDDarkBasement]
                }],
                ComplexRequirementType.DarkRoomPoDDarkMaze => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    SequenceBreakRequirements[SequenceBreakType.DarkRoomPoDDarkMaze]
                }],
                ComplexRequirementType.DarkRoomPoDBossArea => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    SequenceBreakRequirements[SequenceBreakType.DarkRoomPoDBossArea]
                }],
                ComplexRequirementType.DarkRoomMM => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    SequenceBreakRequirements[SequenceBreakType.DarkRoomMM]
                }],
                ComplexRequirementType.DarkRoomTR => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    SequenceBreakRequirements[SequenceBreakType.DarkRoomTR]
                }],
                ComplexRequirementType.FakeFlippersFairyRevival => AggregateRequirements[new HashSet<IRequirement>
                {
                    SequenceBreakRequirements[SequenceBreakType.FakeFlippersFairyRevival],
                    ItemRequirements[(ItemType.Bottle, 1)]
                }],
                ComplexRequirementType.FakeFlippersSplashDeletion => AggregateRequirements[
                    new HashSet<IRequirement>
                    {
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Bow, 1)],
                            ItemRequirements[(ItemType.RedBoomerang, 1)],
                            ItemRequirements[(ItemType.CaneOfSomaria, 1)],
                            ItemRequirements[(ItemType.IceRod, 1)]
                        }],
                        SequenceBreakRequirements[SequenceBreakType.FakeFlippersFairyRevival]
                    }],
                ComplexRequirementType.FireSource => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Lamp, 1)],
                    ItemRequirements[(ItemType.FireRod, 1)]
                }],
                ComplexRequirementType.Hover => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Boots, 1)],
                    SequenceBreakRequirements[SequenceBreakType.Hover]
                }],
                ComplexRequirementType.LaserBridge => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemPlacementRequirements[ItemPlacement.Advanced],
                    ItemRequirements[(ItemType.Cape, 1)],
                    ItemRequirements[(ItemType.CaneOfByrna, 1)],
                    ItemRequirements[(ItemType.Shield, 3)],
                    SequenceBreakRequirements[SequenceBreakType.TRLaserSkip]
                }],
                ComplexRequirementType.MagicBat => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Powder, 1)],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        SequenceBreakRequirements[SequenceBreakType.FakePowder],
                        ItemExactRequirements[(ItemType.Mushroom, 1)],
                        ItemRequirements[(ItemType.CaneOfSomaria, 1)]
                    }]
                }],
                ComplexRequirementType.Pedestal => AlternativeRequirements[new HashSet<IRequirement>
                {
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        PrizeRequirements[(PrizeType.Pendant, 2)],
                        PrizeRequirements[(PrizeType.GreenPendant, 1)],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemPlacementRequirements[ItemPlacement.Advanced],
                            ItemRequirements[(ItemType.Book, 1)],
                            SequenceBreakRequirements[SequenceBreakType.BlindPedestal]
                        }]
                    }],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Book, 1)],
                        StaticRequirements[AccessibilityLevel.Inspect]
                    }]
                }],
                ComplexRequirementType.RedEyegoreGoriya => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Bow, 1)],
                    EnemyShuffleRequirements[true]
                }],
                ComplexRequirementType.SpikeCave => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.CaneOfByrna, 1)],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Cape, 1)],
                        ComplexRequirements[ComplexRequirementType.ExtendMagic1]
                    }]
                }],
                ComplexRequirementType.SuperBunnyMirror => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Mirror, 1)],
                    SequenceBreakRequirements[SequenceBreakType.SuperBunnyMirror]
                }],
                ComplexRequirementType.Tablet => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Book, 1)],
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Sword, 3)],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ItemExactRequirements[(ItemType.Sword, 0)],
                            ItemRequirements[(ItemType.Hammer, 1)]
                        }],
                        StaticRequirements[AccessibilityLevel.Inspect]
                    }]
                }],
                ComplexRequirementType.Torch => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Boots, 1)],
                    StaticRequirements[AccessibilityLevel.Inspect]
                }],
                ComplexRequirementType.ToHHerapot => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Hookshot, 1)],
                    SequenceBreakRequirements[SequenceBreakType.ToHHerapot]
                }],
                ComplexRequirementType.IPIceBreaker => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.CaneOfSomaria, 1)],
                    SequenceBreakRequirements[SequenceBreakType.IPIceBreaker]
                }],
                ComplexRequirementType.MMMedallion => AggregateRequirements[new HashSet<IRequirement>
                {
                    ComplexRequirements[ComplexRequirementType.UseMedallion],
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ComplexRequirements[ComplexRequirementType.AllMedallions],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Bombos, 1)],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                ItemExactRequirements[(ItemType.BombosDungeons, 1)],
                                ItemExactRequirements[(ItemType.BombosDungeons, 3)]
                            }]
                        }],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Ether, 1)],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                ItemExactRequirements[(ItemType.EtherDungeons, 1)],
                                ItemExactRequirements[(ItemType.EtherDungeons, 3)]
                            }]
                        }],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Quake, 1)],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                ItemExactRequirements[(ItemType.QuakeDungeons, 1)],
                                ItemExactRequirements[(ItemType.QuakeDungeons, 3)]
                            }]
                        }],
                    }]
                }],
                ComplexRequirementType.TRMedallion => AggregateRequirements[new HashSet<IRequirement>
                {
                    ComplexRequirements[ComplexRequirementType.UseMedallion],
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ComplexRequirements[ComplexRequirementType.AllMedallions],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Bombos, 1)],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                ItemExactRequirements[(ItemType.BombosDungeons, 2)],
                                ItemExactRequirements[(ItemType.BombosDungeons, 3)]
                            }]
                        }],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Ether, 1)],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                ItemExactRequirements[(ItemType.EtherDungeons, 2)],
                                ItemExactRequirements[(ItemType.EtherDungeons, 3)]
                            }]
                        }],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Quake, 1)],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                ItemExactRequirements[(ItemType.QuakeDungeons, 2)],
                                ItemExactRequirements[(ItemType.QuakeDungeons, 3)]
                            }]
                        }],
                    }]
                }],
                ComplexRequirementType.TRKeyDoorsToMiddleExit => AlternativeRequirements[new HashSet<IRequirement>
                {
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        SmallKeyShuffleRequirements[true],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            SmallKeyRequirements[(DungeonID.TurtleRock, 3)],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                KeyDropShuffleRequirements[false],
                                SmallKeyRequirements[(DungeonID.TurtleRock, 2)]
                            }]
                        }]
                    }],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        SmallKeyShuffleRequirements[false],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.FireRod, 1)],
                            StaticRequirements[AccessibilityLevel.SequenceBreak]
                        }]
                    }]
                }],
                ComplexRequirementType.WaterWalk => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Boots, 1)],
                    SequenceBreakRequirements[SequenceBreakType.WaterWalk]
                }],
                ComplexRequirementType.WaterWalkFromWaterfallCave => AggregateRequirements[
                    new HashSet<IRequirement>
                    {
                        ItemExactRequirements[(ItemType.Flippers, 0)],
                        SequenceBreakRequirements[SequenceBreakType.WaterWalkFromWaterfallCave],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.MoonPearl, 1)],
                            EntranceShuffleRequirements[EntranceShuffle.All],
                            EntranceShuffleRequirements[EntranceShuffle.Insanity]
                        }]
                    }],
                ComplexRequirementType.LWMirror => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Mirror, 1)],
                    WorldStateRequirements[WorldState.Inverted]
                }],
                ComplexRequirementType.DWMirror => AggregateRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Mirror, 1)],
                    WorldStateRequirements[WorldState.StandardOpen]
                }],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            }).ToExpectedObject());
        }
    }

    [Fact]
    public void GetComplexRequirement_ShouldThrowException_WhenTypeIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _ = _sut.GetComplexRequirement((ComplexRequirementType)int.MaxValue));
    }

    [Theory]
    [MemberData(nameof(GetComplexRequirement_ShouldReturnExpectedValueData))]
    public void GetComplexRequirement_ShouldReturnExpectedValue(
        ExpectedObject expected, ComplexRequirementType type)
    {
        expected.ShouldEqual(_sut.GetComplexRequirement(type));
    }

    public static IEnumerable<object[]> GetComplexRequirement_ShouldReturnExpectedValueData()
    {
        PopulateExpectedValues();

        return ExpectedValues.Keys.Select(type => new object[] {ExpectedValues[type], type}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IComplexRequirementFactory.Factory>();
        var sut = factory();
            
        Assert.NotNull(sut as ComplexRequirementFactory);
    }
}