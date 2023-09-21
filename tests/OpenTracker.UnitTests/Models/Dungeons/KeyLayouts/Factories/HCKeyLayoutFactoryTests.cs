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
using OpenTracker.Models.Requirements.Alternative;
using OpenTracker.Models.Requirements.BigKeyShuffle;
using OpenTracker.Models.Requirements.KeyDropShuffle;
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories;

[ExcludeFromCodeCoverage]
public sealed class HCKeyLayoutFactoryTests
{
    private static readonly IAggregateRequirementDictionary AggregateRequirements =
        new AggregateRequirementDictionary(requirements =>
            new AggregateRequirement(requirements));
    private static readonly IAlternativeRequirementDictionary AlternativeRequirements =
        new AlternativeRequirementDictionary(requirements =>
            new AlternativeRequirement(requirements));
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

    private readonly HCKeyLayoutFactory _sut = new(
        AggregateRequirements, AlternativeRequirements, BigKeyShuffleRequirements,
        KeyDropShuffleRequirements, SmallKeyShuffleRequirements,
        BigKeyFactory, EndFactory, SmallKeyFactory);

    [Fact]
    public void GetDungeonKeyLayouts_ShouldReturnExpected()
    {
        var dungeon = Substitute.For<IDungeon>();
            
        var expected = (new List<IKeyLayout>
        {
            EndFactory(
                AggregateRequirements[new HashSet<IRequirement>
                {
                    AlternativeRequirements[new HashSet<IRequirement>
                    {
                        BigKeyShuffleRequirements[true],
                        KeyDropShuffleRequirements[false]
                    }],
                    SmallKeyShuffleRequirements[true]
                }]),
            SmallKeyFactory(
                1, new List<DungeonItemID>
                {
                    DungeonItemID.HCSanctuary,
                    DungeonItemID.HCMapChest,
                    DungeonItemID.HCDarkCross,
                    DungeonItemID.HCSecretRoomLeft,
                    DungeonItemID.HCSecretRoomMiddle,
                    DungeonItemID.HCSecretRoomRight
                },
                false, new List<IKeyLayout> {EndFactory()}, dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    KeyDropShuffleRequirements[false],
                    SmallKeyShuffleRequirements[false]
                }]),
            SmallKeyFactory(
                3, new List<DungeonItemID>
                {
                    DungeonItemID.HCSanctuary,
                    DungeonItemID.HCMapChest,
                    DungeonItemID.HCDarkCross,
                    DungeonItemID.HCSecretRoomLeft,
                    DungeonItemID.HCSecretRoomMiddle,
                    DungeonItemID.HCSecretRoomRight,
                    DungeonItemID.HCMapGuardDrop
                },
                false, new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        4, new List<DungeonItemID>
                        {
                            DungeonItemID.HCSanctuary,
                            DungeonItemID.HCMapChest,
                            DungeonItemID.HCBoomerangChest,
                            DungeonItemID.HCDarkCross,
                            DungeonItemID.HCSecretRoomLeft,
                            DungeonItemID.HCSecretRoomMiddle,
                            DungeonItemID.HCSecretRoomRight,
                            DungeonItemID.HCMapGuardDrop,
                            DungeonItemID.HCBoomerangGuardDrop
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
                    DungeonItemID.HCSanctuary,
                    DungeonItemID.HCMapChest,
                    DungeonItemID.HCBoomerangChest,
                    DungeonItemID.HCDarkCross,
                    DungeonItemID.HCSecretRoomLeft,
                    DungeonItemID.HCSecretRoomMiddle,
                    DungeonItemID.HCSecretRoomRight,
                    DungeonItemID.HCMapGuardDrop,
                    DungeonItemID.HCBoomerangGuardDrop,
                    DungeonItemID.HCBigKeyDrop
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
                    DungeonItemID.HCSanctuary,
                    DungeonItemID.HCMapChest,
                    DungeonItemID.HCDarkCross,
                    DungeonItemID.HCSecretRoomLeft,
                    DungeonItemID.HCSecretRoomMiddle,
                    DungeonItemID.HCSecretRoomRight,
                    DungeonItemID.HCMapGuardDrop
                },
                new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.HCSanctuary,
                            DungeonItemID.HCMapChest,
                            DungeonItemID.HCDarkCross,
                            DungeonItemID.HCSecretRoomLeft,
                            DungeonItemID.HCSecretRoomMiddle,
                            DungeonItemID.HCSecretRoomRight,
                            DungeonItemID.HCMapGuardDrop
                        },
                        true, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.HCSanctuary,
                                    DungeonItemID.HCMapChest,
                                    DungeonItemID.HCBoomerangChest,
                                    DungeonItemID.HCDarkCross,
                                    DungeonItemID.HCSecretRoomLeft,
                                    DungeonItemID.HCSecretRoomMiddle,
                                    DungeonItemID.HCSecretRoomRight,
                                    DungeonItemID.HCMapGuardDrop,
                                    DungeonItemID.HCBoomerangGuardDrop
                                },
                                true, new List<IKeyLayout> {EndFactory()}, dungeon)
                        },
                        dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true],
                    SmallKeyShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID>
                {
                    DungeonItemID.HCBoomerangChest,
                    DungeonItemID.HCBoomerangGuardDrop
                },
                new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.HCSanctuary,
                            DungeonItemID.HCMapChest,
                            DungeonItemID.HCDarkCross,
                            DungeonItemID.HCSecretRoomLeft,
                            DungeonItemID.HCSecretRoomMiddle,
                            DungeonItemID.HCSecretRoomRight,
                            DungeonItemID.HCMapGuardDrop
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.HCSanctuary,
                                    DungeonItemID.HCMapChest,
                                    DungeonItemID.HCBoomerangChest,
                                    DungeonItemID.HCDarkCross,
                                    DungeonItemID.HCSecretRoomLeft,
                                    DungeonItemID.HCSecretRoomMiddle,
                                    DungeonItemID.HCSecretRoomRight,
                                    DungeonItemID.HCMapGuardDrop,
                                    DungeonItemID.HCBoomerangGuardDrop
                                },
                                true, new List<IKeyLayout> {EndFactory()}, dungeon)
                        }, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true],
                    SmallKeyShuffleRequirements[false]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.HCBigKeyDrop}, new List<IKeyLayout>
                {
                    SmallKeyFactory(
                        3, new List<DungeonItemID>
                        {
                            DungeonItemID.HCSanctuary,
                            DungeonItemID.HCMapChest,
                            DungeonItemID.HCDarkCross,
                            DungeonItemID.HCSecretRoomLeft,
                            DungeonItemID.HCSecretRoomMiddle,
                            DungeonItemID.HCSecretRoomRight,
                            DungeonItemID.HCMapGuardDrop
                        },
                        false, new List<IKeyLayout>
                        {
                            SmallKeyFactory(
                                4, new List<DungeonItemID>
                                {
                                    DungeonItemID.HCSanctuary,
                                    DungeonItemID.HCMapChest,
                                    DungeonItemID.HCBoomerangChest,
                                    DungeonItemID.HCDarkCross,
                                    DungeonItemID.HCSecretRoomLeft,
                                    DungeonItemID.HCSecretRoomMiddle,
                                    DungeonItemID.HCSecretRoomRight,
                                    DungeonItemID.HCMapGuardDrop,
                                    DungeonItemID.HCBoomerangGuardDrop
                                },
                                false, new List<IKeyLayout> {EndFactory()}, dungeon)
                        }, dungeon)
                },
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[false],
                    KeyDropShuffleRequirements[true],
                    SmallKeyShuffleRequirements[false]
                }])
        }).ToExpectedObject();
            
        expected.ShouldEqual(_sut.GetDungeonKeyLayouts(dungeon));
    }
}