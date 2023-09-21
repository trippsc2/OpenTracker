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
using OpenTracker.Models.Requirements.SmallKeyShuffle;
using Xunit;

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories;

[ExcludeFromCodeCoverage]
public sealed class ToHKeyLayoutFactoryTests
{
    private static readonly IAggregateRequirementDictionary AggregateRequirements =
        new AggregateRequirementDictionary(requirements =>
            new AggregateRequirement(requirements));
    private static readonly IBigKeyShuffleRequirementDictionary BigKeyShuffleRequirements =
        Substitute.For<IBigKeyShuffleRequirementDictionary>();
    private static readonly IGuaranteedBossItemsRequirementDictionary GuaranteedBossItemsRequirements =
        Substitute.For<IGuaranteedBossItemsRequirementDictionary>();
    private static readonly ISmallKeyShuffleRequirementDictionary SmallKeyShuffleRequirements =
        Substitute.For<ISmallKeyShuffleRequirementDictionary>();

    private static readonly IBigKeyLayout.Factory BigKeyFactory = (bigKeyLocations, children, requirement) =>
        new BigKeyLayout(bigKeyLocations, children, requirement);
    private static readonly IEndKeyLayout.Factory EndFactory = requirement => new EndKeyLayout(requirement);
    private static readonly ISmallKeyLayout.Factory SmallKeyFactory =
        (count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement) => new SmallKeyLayout(
            count, smallKeyLocations, bigKeyInLocations, children, dungeon, requirement);

    private readonly ToHKeyLayoutFactory _sut = new(
        AggregateRequirements, BigKeyShuffleRequirements, GuaranteedBossItemsRequirements,
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
                    DungeonItemID.ToHBasementCage,
                    DungeonItemID.ToHMapChest,
                    DungeonItemID.ToHBigKeyChest,
                    DungeonItemID.ToHCompassChest,
                    DungeonItemID.ToHBigChest,
                    DungeonItemID.ToHBoss
                },
                false, new List<IKeyLayout> {EndFactory()}, dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    GuaranteedBossItemsRequirements[false]
                }]),
            SmallKeyFactory(
                1, new List<DungeonItemID>
                {
                    DungeonItemID.ToHBasementCage,
                    DungeonItemID.ToHMapChest,
                    DungeonItemID.ToHBigKeyChest,
                    DungeonItemID.ToHCompassChest,
                    DungeonItemID.ToHBigChest
                },
                false, new List<IKeyLayout> {EndFactory()}, dungeon,
                AggregateRequirements[new HashSet<IRequirement>
                {
                    BigKeyShuffleRequirements[true],
                    GuaranteedBossItemsRequirements[true]
                }]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.ToHBasementCage, DungeonItemID.ToHMapChest},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest,
                            DungeonItemID.ToHCompassChest,
                            DungeonItemID.ToHBigChest,
                            DungeonItemID.ToHBoss
                        },
                        true, new List<IKeyLayout> {EndFactory()}, dungeon,
                        GuaranteedBossItemsRequirements[false]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest,
                            DungeonItemID.ToHBigKeyChest,
                            DungeonItemID.ToHCompassChest,
                            DungeonItemID.ToHBigChest
                        },
                        true, new List<IKeyLayout> {EndFactory()}, dungeon,
                        GuaranteedBossItemsRequirements[true])
                },
                BigKeyShuffleRequirements[false]),
            BigKeyFactory(
                new List<DungeonItemID> {DungeonItemID.ToHBigKeyChest},
                new List<IKeyLayout>
                {
                    EndFactory(SmallKeyShuffleRequirements[true]),
                    SmallKeyFactory(
                        1, new List<DungeonItemID>
                        {
                            DungeonItemID.ToHBasementCage,
                            DungeonItemID.ToHMapChest
                        },
                        false, new List<IKeyLayout> {EndFactory()}, dungeon)
                },
                BigKeyShuffleRequirements[false])
        }).ToExpectedObject();
            
        expected.ShouldEqual(_sut.GetDungeonKeyLayouts(dungeon));
    }
}