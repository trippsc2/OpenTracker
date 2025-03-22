using System.Collections.Generic;
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

namespace OpenTracker.UnitTests.Models.Dungeons.KeyLayouts.Factories
{
    public class EPKeyLayoutFactoryTests
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

        private readonly EPKeyLayoutFactory _sut = new(
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
                        BigKeyShuffleRequirements[true],
                        AlternativeRequirements[new HashSet<IRequirement>
                        {
                            KeyDropShuffleRequirements[false],
                            SmallKeyShuffleRequirements[true]
                        }]
                    }]),
                BigKeyFactory(
                    new List<DungeonItemID>
                    {
                        DungeonItemID.EPCannonballChest,
                        DungeonItemID.EPMapChest,
                        DungeonItemID.EPCompassChest,
                        DungeonItemID.EPBigKeyChest
                    },
                    new List<IKeyLayout> {EndFactory()},
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        BigKeyShuffleRequirements[false],
                        KeyDropShuffleRequirements[false]
                    }]),
                SmallKeyFactory(
                    1, new List<DungeonItemID>
                    {
                        DungeonItemID.EPCannonballChest,
                        DungeonItemID.EPMapChest,
                        DungeonItemID.EPCompassChest,
                        DungeonItemID.EPBigChest,
                        DungeonItemID.EPDarkSquarePot
                    },
                    false, new List<IKeyLayout>
                    {
                        SmallKeyFactory(
                            2, new List<DungeonItemID>
                            {
                                DungeonItemID.EPCannonballChest,
                                DungeonItemID.EPMapChest,
                                DungeonItemID.EPCompassChest,
                                DungeonItemID.EPBigChest,
                                DungeonItemID.EPBigKeyChest,
                                DungeonItemID.EPDarkSquarePot,
                                DungeonItemID.EPDarkEyegoreDrop
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
                        DungeonItemID.EPCannonballChest,
                        DungeonItemID.EPMapChest,
                        DungeonItemID.EPCompassChest,
                        DungeonItemID.EPDarkSquarePot
                    },
                    new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.EPCannonballChest,
                                DungeonItemID.EPMapChest,
                                DungeonItemID.EPCompassChest,
                                DungeonItemID.EPDarkSquarePot,
                                DungeonItemID.EPBigChest,
                                DungeonItemID.EPDarkEyegoreDrop
                            },
                            true, new List<IKeyLayout>
                            {
                                SmallKeyFactory(
                                    2, new List<DungeonItemID>
                                    {
                                        DungeonItemID.EPCannonballChest,
                                        DungeonItemID.EPMapChest,
                                        DungeonItemID.EPCompassChest,
                                        DungeonItemID.EPBigChest,
                                        DungeonItemID.EPBigKeyChest,
                                        DungeonItemID.EPDarkSquarePot,
                                        DungeonItemID.EPDarkEyegoreDrop
                                    },
                                    true, new List<IKeyLayout> {EndFactory()}, dungeon),
                            },
                            dungeon)
                    },
                    AggregateRequirements[new HashSet<IRequirement>
                    {
                        BigKeyShuffleRequirements[false],
                        KeyDropShuffleRequirements[true]
                    }]),
                BigKeyFactory(
                    new List<DungeonItemID> {DungeonItemID.EPBigKeyChest}, new List<IKeyLayout>
                    {
                        EndFactory(SmallKeyShuffleRequirements[true]),
                        SmallKeyFactory(
                            1, new List<DungeonItemID>
                            {
                                DungeonItemID.EPCannonballChest,
                                DungeonItemID.EPMapChest,
                                DungeonItemID.EPCompassChest,
                                DungeonItemID.EPDarkSquarePot
                            },
                            false, new List<IKeyLayout>
                            {
                                SmallKeyFactory(
                                    2, new List<DungeonItemID>
                                    {
                                        DungeonItemID.EPCannonballChest,
                                        DungeonItemID.EPMapChest,
                                        DungeonItemID.EPCompassChest,
                                        DungeonItemID.EPDarkSquarePot,
                                        DungeonItemID.EPBigChest,
                                        DungeonItemID.EPDarkEyegoreDrop
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
}