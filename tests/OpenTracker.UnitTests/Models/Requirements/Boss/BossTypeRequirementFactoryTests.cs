using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Autofac;
using ExpectedObjects;
using NSubstitute;
using OpenTracker.Models.Accessibility;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Items;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Requirements.Aggregate;
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.Boss;
using OpenTracker.Models.Requirements.Complex;
using OpenTracker.Models.Requirements.Item;
using OpenTracker.Models.Requirements.Item.Exact;
using OpenTracker.Models.Requirements.Mode;
using OpenTracker.Models.Requirements.SequenceBreak;
using OpenTracker.Models.Requirements.Static;
using OpenTracker.Models.SequenceBreaks;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements.Boss;

[ExcludeFromCodeCoverage]
public sealed class BossTypeRequirementFactoryTests
{
    // ReSharper disable once CollectionNeverUpdated.Local
    private static readonly IAggregateRequirementDictionary AggregateRequirements =
        new AggregateRequirementDictionary(
            requirements => new AggregateRequirement(requirements));
    // ReSharper disable once CollectionNeverUpdated.Local
    private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
        new AlternativeRequirementDictionary(
            requirements => new AlternativeRequirement(requirements));
    private static readonly IBossTypeRequirementDictionary BossTypeRequirements =
        Substitute.For<IBossTypeRequirementDictionary>();
    private static readonly IComplexRequirementDictionary ComplexRequirements =
        Substitute.For<IComplexRequirementDictionary>();
    private static readonly IItemRequirementDictionary ItemRequirements =
        Substitute.For<IItemRequirementDictionary>();
    private static readonly IItemExactRequirementDictionary ItemExactRequirements =
        Substitute.For<IItemExactRequirementDictionary>();
    private static readonly IItemPlacementRequirementDictionary ItemPlacementRequirements =
        Substitute.For<IItemPlacementRequirementDictionary>();
    private static readonly ISequenceBreakRequirementDictionary SequenceBreakRequirements =
        Substitute.For<ISequenceBreakRequirementDictionary>();
    private static readonly IStaticRequirementDictionary StaticRequirements =
        Substitute.For<IStaticRequirementDictionary>();
        
    private static readonly Dictionary<BossType, ExpectedObject> ExpectedValues = new();

    private readonly BossTypeRequirementFactory _sut;

    public BossTypeRequirementFactoryTests()
    {
        _sut = new BossTypeRequirementFactory(
            AggregateRequirements, AlternativeRequirements, BossTypeRequirements, ComplexRequirements,
            ItemRequirements, ItemExactRequirements, ItemPlacementRequirements, SequenceBreakRequirements,
            StaticRequirements);
    }
        
    private static void PopulateExpectedValues()
    {
        ExpectedValues.Clear();

        foreach (BossType type in Enum.GetValues(typeof(BossType)))
        {
            ExpectedValues.Add(type, (type switch
            {
                BossType.Test => StaticRequirements[AccessibilityLevel.Normal],
                BossType.Armos => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Sword, 2)],
                    ItemRequirements[(ItemType.Hammer, 1)],
                    ItemRequirements[(ItemType.Bow, 1)],
                    ItemRequirements[(ItemType.Boomerang, 1)],
                    ItemRequirements[(ItemType.RedBoomerang, 1)],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        ComplexRequirements[ComplexRequirementType.ExtendMagic2],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.FireRod, 1)],
                            ItemRequirements[(ItemType.IceRod, 1)]
                        }]
                    }],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        ComplexRequirements[ComplexRequirementType.ExtendMagic1],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.CaneOfByrna, 1)],
                            ItemRequirements[(ItemType.CaneOfSomaria, 1)]
                        }]
                    }]
                }],
                BossType.Lanmolas => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Sword, 2)],
                    ItemRequirements[(ItemType.Hammer, 1)],
                    ItemRequirements[(ItemType.Bow, 1)],
                    ItemRequirements[(ItemType.FireRod, 1)],
                    ItemRequirements[(ItemType.IceRod, 1)],
                    ItemRequirements[(ItemType.CaneOfByrna, 1)],
                    ItemRequirements[(ItemType.CaneOfSomaria, 1)],
                    SequenceBreakRequirements[SequenceBreakType.LanmolasBombs]
                }],
                BossType.Moldorm => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Sword, 2)],
                    ItemRequirements[(ItemType.Hammer, 1)]
                }],
                BossType.HelmasaurKing => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Sword, 3)],
                    ItemRequirements[(ItemType.Bow, 1)],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Sword, 2)],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemPlacementRequirements[ItemPlacement.Advanced],
                            SequenceBreakRequirements[SequenceBreakType.HelmasaurKingBasic]
                        }]
                    }],
                }],
                BossType.Arrghus => AggregateRequirements[new HashSet<IRequirement>
                {
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemPlacementRequirements[ItemPlacement.Advanced],
                        ItemExactRequirements[(ItemType.Sword, 0)],
                        ItemRequirements[(ItemType.Sword, 3)],
                        SequenceBreakRequirements[SequenceBreakType.ArrghusBasic]
                    }],
                    ItemRequirements[(ItemType.Hookshot, 1)],
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Hammer, 1)],
                        ItemRequirements[(ItemType.Sword, 2)],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                ComplexRequirements[ComplexRequirementType.ExtendMagic1],
                                ItemRequirements[(ItemType.Bow, 1)]
                            }],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                ItemRequirements[(ItemType.FireRod, 1)],
                                ItemRequirements[(ItemType.IceRod, 1)]
                            }]
                        }]
                    }]
                }],
                BossType.Mothula => AggregateRequirements[new HashSet<IRequirement>
                {
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemPlacementRequirements[ItemPlacement.Advanced],
                        ItemRequirements[(ItemType.Sword, 3)],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ComplexRequirements[ComplexRequirementType.ExtendMagic1],
                            ItemRequirements[(ItemType.FireRod, 1)]
                        }],
                        SequenceBreakRequirements[SequenceBreakType.MothulaBasic]
                    }],
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Sword, 2)],
                        ItemRequirements[(ItemType.Hammer, 1)],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ComplexRequirements[ComplexRequirementType.ExtendMagic1],
                            ItemRequirements[(ItemType.FireRod, 1)]
                        }],
                        ItemRequirements[(ItemType.CaneOfSomaria, 1)],
                        ItemRequirements[(ItemType.CaneOfByrna, 1)]
                    }]
                }],
                BossType.Blind => AggregateRequirements[new HashSet<IRequirement>
                {
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemPlacementRequirements[ItemPlacement.Advanced],
                        ItemExactRequirements[(ItemType.Sword, 0)],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Sword, 2)],
                            AlternativeRequirements[new HashSet<IRequirement>
                            {
                                ItemRequirements[(ItemType.Cape, 1)],
                                ItemRequirements[(ItemType.CaneOfByrna, 1)]
                            }]
                        }],
                        SequenceBreakRequirements[SequenceBreakType.BlindBasic]
                    }],
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Sword, 2)],
                        ItemRequirements[(ItemType.Hammer, 1)],
                        ItemRequirements[(ItemType.CaneOfSomaria, 1)],
                        ItemRequirements[(ItemType.CaneOfByrna, 1)]
                    }]
                }],
                BossType.Kholdstare => AggregateRequirements[new HashSet<IRequirement>
                {
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemPlacementRequirements[ItemPlacement.Advanced],
                        ItemRequirements[(ItemType.Sword, 3)],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ComplexRequirements[ComplexRequirementType.ExtendMagic2],
                            ItemRequirements[(ItemType.FireRod, 1)]
                        }],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Bombos, 1)],
                            ComplexRequirements[ComplexRequirementType.UseMedallion],
                            ComplexRequirements[ComplexRequirementType.ExtendMagic1],
                            ItemRequirements[(ItemType.FireRod, 1)]
                        }],
                        SequenceBreakRequirements[SequenceBreakType.KholdstareBasic]
                    }],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        ComplexRequirements[ComplexRequirementType.MeltThings],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Hammer, 1)],
                            ItemRequirements[(ItemType.Sword, 2)],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                ComplexRequirements[ComplexRequirementType.ExtendMagic2],
                                ItemRequirements[(ItemType.FireRod, 1)]
                            }],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                ItemExactRequirements[(ItemType.Sword, 0)],
                                ComplexRequirements[ComplexRequirementType.ExtendMagic1],
                                ItemRequirements[(ItemType.FireRod, 1)],
                                ItemRequirements[(ItemType.Bombos, 1)]
                            }]
                        }]
                    }]
                }],
                BossType.Vitreous => AggregateRequirements[new HashSet<IRequirement>
                {
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemPlacementRequirements[ItemPlacement.Advanced],
                        ItemRequirements[(ItemType.Sword, 3)],
                        ItemRequirements[(ItemType.Bow, 1)],
                        SequenceBreakRequirements[SequenceBreakType.VitreousBasic]
                    }],
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.Hammer, 1)],
                        ItemRequirements[(ItemType.Sword, 2)],
                        ItemRequirements[(ItemType.Bow, 1)]
                    }]
                }],
                BossType.Trinexx => AggregateRequirements[new HashSet<IRequirement>
                {
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        ItemPlacementRequirements[ItemPlacement.Advanced],
                        ItemExactRequirements[(ItemType.Sword, 0)],
                        ItemRequirements[(ItemType.Sword, 4)],
                        AggregateRequirements[new HashSet<IRequirement>
                        {
                            ComplexRequirements[ComplexRequirementType.ExtendMagic1],
                            ItemRequirements[(ItemType.Sword, 3)]
                        }],
                        SequenceBreakRequirements[SequenceBreakType.TrinexxBasic]
                    }],
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        ItemRequirements[(ItemType.FireRod, 1)],
                        ItemRequirements[(ItemType.IceRod, 1)],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            ItemRequirements[(ItemType.Sword, 4)],
                            ItemRequirements[(ItemType.Hammer, 1)],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                ComplexRequirements[ComplexRequirementType.ExtendMagic1],
                                ItemRequirements[(ItemType.Sword, 3)]
                            }],
                            AggregateRequirements[new HashSet<IRequirement>
                            {
                                ComplexRequirements[ComplexRequirementType.ExtendMagic2],
                                ItemRequirements[(ItemType.Sword, 2)]
                            }]
                        }]
                    }]
                }],
                BossType.Aga => AlternativeRequirements[new HashSet<IRequirement>
                {
                    ItemRequirements[(ItemType.Sword, 2)],
                    ItemRequirements[(ItemType.Hammer, 1)],
                    ItemRequirements[(ItemType.Net, 1)]
                }],
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            }).ToExpectedObject());
        }
    }

    [Fact]
    public void GetBossTypeRequirement_ShouldThrowException_WhenBossTypeIsUnexpected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _ = _sut.GetBossTypeRequirement((BossType)int.MaxValue));
    }

    [Fact]
    public void GetBossTypeRequirement_ShouldReturnExpectedValue_WhenBossTypeIsNull()
    {
        var expected = AlternativeRequirements[new HashSet<IRequirement>
        {
            AggregateRequirements[new HashSet<IRequirement>
            {
                BossTypeRequirements[BossType.Armos],
                BossTypeRequirements[BossType.Lanmolas],
                BossTypeRequirements[BossType.Moldorm],
                BossTypeRequirements[BossType.HelmasaurKing],
                BossTypeRequirements[BossType.Arrghus],
                BossTypeRequirements[BossType.Mothula],
                BossTypeRequirements[BossType.Blind],
                BossTypeRequirements[BossType.Kholdstare],
                BossTypeRequirements[BossType.Vitreous],
                BossTypeRequirements[BossType.Trinexx]
            }],
            StaticRequirements[AccessibilityLevel.SequenceBreak]
        }].ToExpectedObject();
            
        expected.ShouldEqual(_sut.GetBossTypeRequirement(null));
    }

    [Theory]
    [MemberData(nameof(GetBossTypeRequirement_ShouldReturnExpectedValue_WhenBossTypeIsNotNullData))]
    public void GetBossTypeRequirement_ShouldReturnExpectedValue_WhenBossTypeIsNotNull(
        ExpectedObject expected, BossType type)
    {
        expected.ShouldEqual(_sut.GetBossTypeRequirement(type));
    }

    public static IEnumerable<object[]> GetBossTypeRequirement_ShouldReturnExpectedValue_WhenBossTypeIsNotNullData()
    {
        PopulateExpectedValues();

        return ExpectedValues.Keys.Select(type => new object[] {ExpectedValues[type], type}).ToList();
    }

    [Fact]
    public void AutofacTest()
    {
        using var scope = ContainerConfig.Configure().BeginLifetimeScope();
        var factory = scope.Resolve<IBossTypeRequirementFactory.Factory>();
        var sut = factory();
            
        Assert.NotNull(sut as BossTypeRequirementFactory);
    }
}